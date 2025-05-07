using DataRepository;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class AccountRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[Account]"), IAccountRepository
    {
        public void CreateAccount(AccountDbo account)
        {
            var sql = $"INSERT INTO dbo.{this.TableName} "
                      + $"  (Username, Email, Password, Description)"
                      + $"VALUES("
                      + $"  @username, @email, @password, @description);";

            var parameter = new
            {
                username = account.Username,
                email = account.Email,
                password = account.Password,
                description = account.Description,
            };

            this.Insert<AccountDbo>(sql, parameter);
        }

        public void DeleteAccount(string username)
        {
            var sql = $"DELETE FROM dbo.{this.TableName} "
                      + "WHERE Username = @username";

            var parameter = new
            {
                username = username
            };

            this.Execute<AccountDbo>(sql, parameter);
        }

        public void UpdateUsername(string username, string newUsername)
        {
            var sql = $"UPDATE dbo.{this.TableName} "
                      + $"SET Username = @newUsername "
                      + $"WHERE Username = @oldUsername;";

            var parameter = new
            {
                newUsername = newUsername,
                oldUsername = username
            };

            this.Execute<AccountDbo>(sql, parameter);
        }

        public void UpdateEmail(string username, string newEmail)
        {
            var sql = $"UPDATE dbo.{this.TableName} "
                      + $"SET Email = @newEmail "
                      + $"WHERE Username = @username;";

            var parameter = new
            {
                username = username,
                newEmail = newEmail
            };

            this.Execute<AccountDbo>(sql, parameter);
        }

        public void UpdatePassword(string username, string newPassword)
        {
            var sql = $"UPDATE dbo.{this.TableName} "
                      + $"SET Password = @newPassword "
                      + $"WHERE Username = @username;";

            var parameter = new
            {
                username = username,
                newPassword = newPassword
            };

            this.Execute<AccountDbo>(sql, parameter);
        }

        public void UpdateDescription(string username, string newDescription)
        {
            var sql = $"UPDATE dbo.{this.TableName} "
                      + $"SET Description = @newDescription "
                      + $"WHERE Username = @username;";

            var parameter = new
            {
                username = username,
                newDescription = newDescription
            };

            this.Execute<AccountDbo>(sql, parameter);
        }

        public AccountDbo GetAccountByUsername(string username)
        {
            return this.GetByUsername<AccountDbo>(username);
        }

        public AccountDbo GetAccountByEmail(string email)
        {
            return this.GetByEmail<AccountDbo>(email);
        }

        public string? GetPasswordForAccount(string username)
        {
            return GetByUsername<AccountDbo>(username).Password;
        }
    }
}
