using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\.."));
        private readonly string filePath;

        public AccountRepository()
        {
            filePath = Path.Combine(projectPath, "Data/accounts.csv");
        }

        public void AddAccount(AccountDbo account)
        {
            AddAccountToFile(account);
        }
        public void DeleteAccount(string username)
        {
            DeleteAccountFromFile(username);
        }

        public void UpdateAccount(string oldUsername, AccountDbo newAccountData)
        {
            UpdateAccountInFile(oldUsername, newAccountData);
        }

        public AccountDbo? GetAccountByUsername(string username)
        {
            return GetAccountByUsernameFromFile(username);
        }
        public AccountDbo? GetAccountByEmail(string email)
        {
            return GetAccountByEmailFromFile(email);
        }

        #region privates
        public void UpdateAccountInFile(string oldUsername, AccountDbo newAccountData)
        {
            var accounts = GetAllAccounts();
            foreach (var account in accounts.Where(account => account.Username.Equals(oldUsername)))
            {
                account.Username = newAccountData.Username;
                account.Password = newAccountData.Password;
                account.Email = newAccountData.Email;
                account.Description = newAccountData.Description;
            }
            SaveAllAccounts(accounts);
        }

        private void DeleteAccountFromFile(string username)
        {
            var accounts = GetAllAccounts();
            var newAccounts = accounts.Where(account => !account.Username.Equals(username)).ToList();
            SaveAllAccounts(newAccounts);
        }

        private void AddAccountToFile(AccountDbo account)
        {
            var accounts = GetAllAccounts();
            accounts.Add(account);
            SaveAllAccounts(accounts);
        }

        private void SaveAllAccounts(List<AccountDbo> accounts)
        {
            using var writer = new StreamWriter(filePath);
            foreach (var account in accounts)
            {
                writer.WriteLine(ParseToCsvLine(account));
            }
        }

        private static string ParseToCsvLine(AccountDbo account)
        {
            return account.Username + ";" + account.Email + ";" + account.Password + ";" + account.Description;
        }

        private AccountDbo? GetAccountByUsernameFromFile(string username)
        {
            var accounts = GetAllAccounts();
            return accounts.FirstOrDefault(acc => acc.Username.Equals(username));
        }

        private AccountDbo? GetAccountByEmailFromFile(string email)
        {
            var accounts = GetAllAccounts();
            return accounts.FirstOrDefault(acc => acc.Email.Equals(email));
        }

        private List<AccountDbo> GetAllAccounts()
        {
            using var reader = new StreamReader(filePath);
            var accounts = new List<AccountDbo>();
            while (reader.ReadLine() is { } line)
            {
                accounts.Add(ParseToDbo(line));
            }
            return accounts;
        }

        private static AccountDbo ParseToDbo(string line)
        {
            var elements = line.Split(';');
            return new AccountDbo(elements[0], elements[1], elements[2], elements[3]);
        }
        #endregion
    }
}
