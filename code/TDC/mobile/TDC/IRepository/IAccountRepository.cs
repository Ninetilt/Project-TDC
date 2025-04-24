using TDC.Models;

namespace TDC.IRepository;
public interface IAccountRepository
{
    public Account GetAccountById(long id);
    public void CreateAccount(Account account);
    public void UpdateAccount(Account account);
    public void DeleteAccount(Account account);
    public bool EmailIsTaken(string email);
    public bool UsernameIsTaken(string username);
    public Account? AuthenticateUser(string username, string password);

}
