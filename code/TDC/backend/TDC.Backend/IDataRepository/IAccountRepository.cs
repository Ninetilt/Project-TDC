using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.IDataRepository
{
    public interface IAccountRepository
    {
        public void CreateAccount(AccountDbo account);
        public void DeleteAccount(string username);
        public void UpdateUsername(string username, string newUsername);
        public void UpdateEmail(string username, string newEmail);
        public void UpdatePassword(string username, string newPassword);
        public void UpdateDescription(string username, string newDescription);
        public bool AccountExists(string username);
        public bool AccountWithEmailExists(string email);
        public AccountDbo? GetAccountByUsername(string username);
        public AccountDbo? GetAccountByEmail(string email);
    }
}
