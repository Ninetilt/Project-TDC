using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDomain;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Domain
{
    public class AccountHandler(IAccountRepository accountRepository) : IAccountHandler
    {
        public readonly IAccountRepository _accountRepository = accountRepository;

        public Task AcceptFriendRequest(string username, string requestName)
        {
            throw new NotImplementedException();
        }

        public Task CancelFriendRequest(string sender, string receiver)
        {
            throw new NotImplementedException();
        }

        public Task DenyFriendRequest(string username, string requestName)
        {
            throw new NotImplementedException();
        }

        public AccountLoadingDto GetAccountByUsername(string username)
        {
            var accountDbo = _accountRepository.GetAccountByUsername(username)!;
            return new AccountLoadingDto(accountDbo.Username, accountDbo.Email, accountDbo.Description);
        }

        public List<string> GetFriendsForUser(string username)
        {
            throw new NotImplementedException();
        }

        public List<string> GetRequestsForUser(string username)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
