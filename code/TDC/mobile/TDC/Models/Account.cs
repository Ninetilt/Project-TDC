namespace TDC.Models;
public class Account: Profile
{
    public string Email { get; set; }
    public string Password { get; set; }
    public List<long> Friends { get; set; }
    public List<long> Requests { get; set; }

    #region constructors
    public Account(long userId, string username, string description, string email, string password, Character character, List<long> friends, List<long> requests): base(userId, username, description, character)
    {
        Email = email;
        Password = password;
        Friends = friends;
        Requests = requests;
    }
    #endregion
}

