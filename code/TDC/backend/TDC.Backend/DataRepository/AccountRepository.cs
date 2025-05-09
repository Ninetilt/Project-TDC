﻿using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\.."));
        private readonly string filePath;

        public AccountRepository()
        {
            var directoryPath = Path.Combine(projectPath, "TestData");
            if (!Directory.Exists(directoryPath)) { Directory.CreateDirectory(directoryPath); }

            filePath = Path.Combine(directoryPath, "accounts.csv");
            if (!File.Exists(filePath)) { File.WriteAllText(filePath, string.Empty); }
        }

        public void CreateAccount(AccountDbo account)
        {
            AddAccountToFile(account);
        }
        public void DeleteAccount(string username)
        {
            DeleteAccountFromFile(username);
        }

        public void UpdateUsername(string username, string newUsername)
        {
            UpdateUsernameInFile(username, newUsername);
        }
        public void UpdateEmail(string username, string newEmail)
        {
            UpdateEmailInFile(username, newEmail);
        }
        public void UpdatePassword(string username, string newPassword)
        {
            UpdatePasswordInFile(username, newPassword);
        }
        public void UpdateDescription(string username, string newDescription)
        {
            UpdateDescriptionInFile(username, newDescription);
        }

        public bool AccountExists(string username)
        {
            var accounts = GetAllAccounts();
            return accounts.Any(acc => acc.Username.Equals(username));
        }

        public bool AccountWithEmailExists(string email)
        {
            var accounts = GetAllAccounts();
            return accounts.Any(accounts => accounts.Email.Equals(email));
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
        public void UpdateUsernameInFile(string username, string newUsername)
        {
            var accounts = GetAllAccounts();
            foreach (var account in accounts.Where(account => account.Username.Equals(username)))
            {
                account.Username = newUsername;
            }
            SaveAllAccounts(accounts);
        }

        public void UpdateEmailInFile(string username, string newEmail)
        {
            var accounts = GetAllAccounts();
            foreach (var account in accounts.Where(account => account.Username.Equals(username)))
            {
                account.Email = newEmail;
            }
            SaveAllAccounts(accounts);
        }

        public void UpdatePasswordInFile(string username, string newPassword)
        {
            var accounts = GetAllAccounts();
            foreach (var account in accounts.Where(account => account.Username.Equals(username)))
            {
                account.Password = newPassword;
            }
            SaveAllAccounts(accounts);
        }

        public void UpdateDescriptionInFile(string username, string newDescription)
        {
            var accounts = GetAllAccounts();
            foreach (var account in accounts.Where(account => account.Username.Equals(username)))
            {
                account.Description = newDescription;
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
            writer.Close();
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
            reader.Close();
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
