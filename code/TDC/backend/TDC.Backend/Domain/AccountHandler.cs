using System.Reflection;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDomain;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Domain
{
    public class AccountHandler(IAccountRepository accountRepository, IFriendRepository friendRepository, IFriendRequestRepository friendRequestRepository) : IAccountHandler
    {
        public readonly IAccountRepository _accountRepository = accountRepository;
        public readonly IFriendRepository friendRepository = friendRepository;
        public readonly IFriendRequestRepository friendRequestRepository = friendRequestRepository;

        public Task AcceptFriendRequest(string username, string requestName)
        {
            var friends = friendRepository.GetFriendsForUser(username);

            if (friends.Contains(requestName)) {
                return Task.CompletedTask;
            }
            this.friendRepository.AddFriend(username, requestName);
            this.friendRepository.AddFriend(requestName, username);

            this.friendRequestRepository.DeleteFriendRequest(username, requestName);
            this.friendRequestRepository.DeleteFriendRequest(requestName, username);
            
            return Task.CompletedTask;
        }

        public Task CancelFriendRequest(string sender, string receiver)
        {
            this.friendRequestRepository.DeleteFriendRequest(sender, receiver);
            return Task.CompletedTask;
        }

        public Task DenyFriendRequest(string username, string requestName)
        {
            this.friendRequestRepository.DeleteFriendRequest(username, requestName);
            return Task.CompletedTask;
        }

        public AccountLoadingDto GetAccountByUsername(string username)
        {
            var accountDbo = _accountRepository.GetAccountByUsername(username)!;
            return new AccountLoadingDto(accountDbo.Username, accountDbo.Email, accountDbo.Description);
        }

        public List<string> GetFriendsForUser(string username)
        {
            return friendRepository.GetFriendsForUser(username);
        }

        public List<string> GetRequestsForUser(string username)
        {
            return friendRequestRepository.GetRequestsForUser(username);
        }

        public bool LoginWithMail(string email, string password)
        {
            if (!AccountWithEmailExists(email)) { return false;}
            var accountDbo = _accountRepository.GetAccountByEmail(email)!;
            return accountDbo.Password.Equals(password);
        }

        public bool LoginWithUsername(string username, string password)
        {
            if (!AccountWithUsernameExists(username)) { return false;}
            var accountDbo = _accountRepository.GetAccountByUsername(username)!;
            return accountDbo.Password.Equals(password);
        }

        public bool RegisterUser(AccountSavingDto accountData)
        {
            if (AccountWithUsernameExists(accountData.Username)) { return false; }
            if (AccountWithEmailExists(accountData.Email)) { return false; }
            _accountRepository.CreateAccount(new AccountDbo(accountData.Username, accountData.Email, accountData.Password, accountData.Description));
            return true;
        }

        public Task SendFriendRequest(string sender, string receiver)
        {
            var requests = friendRequestRepository.GetRequestsForUser(receiver);
            var friends = friendRepository.GetFriendsForUser(receiver);
            if (requests.Contains(sender)) { return Task.CompletedTask; }
            if (friends.Contains(sender)) { return Task.CompletedTask; }

            this.friendRequestRepository.AddFriendRequest(receiver, sender);
            return Task.CompletedTask;
        }

        public bool UpdateEmail(string username, string email)
        {
            if (AccountWithEmailExists(email)) { return false; }
            _accountRepository.UpdateEmail(username, email);
            return true;
        }

        public bool UpdatePassword(string username, string password)
        {
            if (!AccountWithUsernameExists(username)) { return false;}

            var oldPassword = _accountRepository.GetAccountByUsername(username).Password;
            if(oldPassword!.Equals(password)) { return false; }
            
            _accountRepository.UpdatePassword(username, password);
            return true;
        }

        public bool UpdateUserDescription(string username, string description)
        {
            if (!AccountWithUsernameExists(username)) { return false; }

            _accountRepository.UpdateDescription(username, description);
            return true;
        }

        public bool UpdateUsername(string oldUsername, string newUsername)
        {
            if (!AccountWithUsernameExists(oldUsername)) { return false; }
            if (AccountWithUsernameExists(newUsername)) { return false; }
            _accountRepository.UpdateUsername(oldUsername, newUsername);
            return true;
        }

        #region privates
        private bool AccountWithUsernameExists(string username)
        {
            return _accountRepository.GetAccountByUsername(username) != null;
        }

        private bool AccountWithEmailExists(string email)
        {
            return _accountRepository.GetAccountByEmail(email) != null;
        }
        #endregion
    }
}
