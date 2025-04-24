namespace TDC.Backend.IDataRepository.Models
{
    public class AccountDbo(string username, string email, string password, string description)
    {
        public string Username { get; set; } = username;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public string Description { get; set; } = description;
    }
}
