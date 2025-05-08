namespace TDC.Backend.IDataRepository.Models
{
    public class AccountDbo
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }

        public AccountDbo() { }

        public AccountDbo(string username, string email, string password, string description)
        {
            Username = username;
            Email = email;
            Password = password;
            Description = description;
        }
    }
}
