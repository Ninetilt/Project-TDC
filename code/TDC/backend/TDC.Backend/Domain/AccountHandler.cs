using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDomain;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Domain
{
    public class AccountHandler : IAccountHandler
    {
        internal readonly IAccountRepository _accountRepository;

        public AccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

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
            if (!_accountRepository.AccountWithEmailExists(email)) { return false; }

            var accountDbo = _accountRepository.GetAccountByEmail(email)!;
            if (accountDbo.Password.Equals(password)) {
                return true;
            }
            return false;
        }

        public bool LoginWithUsername(string username, string password)
        {
            if (!_accountRepository.AccountExists(username)) { return false; }

            var accountDbo = _accountRepository.GetAccountByUsername(username)!;
            if (accountDbo.Password.Equals(password))
            {
                return true;
            }
            return false;
        }

        public bool RegisterUser(AccountSavingDto accountData)
        {
            if(UsernameAlreadyExists(accountData.Username)) { return false; }
            if(EmailAlreadyExists(accountData.Email)) { return false; }
            _accountRepository.CreateAccount(new AccountDbo(accountData.Username, accountData.Email, accountData.Password, accountData.Description));
            return true;
        }

        public Task SendFriendRequest(string sender, string receiver)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEmail(string username, string email)
        {
            if(EmailAlreadyExists(email)) { return false; }
            _accountRepository.UpdateEmail(username, email);
            return true;
        }

        public bool UpdatePassword(string username, string password)
        {
            _accountRepository.UpdatePassword(username, password);
            //TO-DO: catch possible errors and return false if update failed -> check error case of existing password and sql exception
            // existing password could also be checked in FE -> discuss
            return true;
        }

        public Task UpdateUserDescription(string username, string description)
        {
            _accountRepository.UpdateDescription(username, description);
            return Task.CompletedTask;
        }

        public bool UpdateUsername(string oldUsername, string newUsername)
        {
            if (UsernameAlreadyExists(newUsername)) { return false; }
            _accountRepository.UpdateUsername(oldUsername, newUsername);
            return true;
        }

        #region privates
        private bool UsernameAlreadyExists(string username)
        {
            return _accountRepository.AccountExists(username);
        }

        private bool EmailAlreadyExists(string email)
        {
            var account = _accountRepository.GetAccountByEmail(email);
            return account != null;
        }
        #endregion
    }
}
