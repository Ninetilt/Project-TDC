using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.IDataRepository
{
    public interface IAccountRepository
    {
        public void AddAccount(AccountDbo account);
        public void DeleteAccount(string username);
        public void UpdateAccount(AccountDbo account);
        public AccountDbo GetAccountByUsername(string username);
        public AccountDbo GetAccountByEmail(string email);
    }
}
