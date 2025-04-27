namespace TDC.Backend.IDomain.Models;

public class AccountLoadingDto(string username, string email, string description)
{
    public string Username { get; set; } = username;
    public string Email { get; set; } = email;
    public string Description { get; set; } = description;
}
