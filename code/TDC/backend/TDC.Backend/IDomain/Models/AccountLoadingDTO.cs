namespace TDC.Backend.IDomain.Models;

public class AccountLoadingDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public List<string> Friends { get; set; }
    public List<string> Requests { get; set; }
}
