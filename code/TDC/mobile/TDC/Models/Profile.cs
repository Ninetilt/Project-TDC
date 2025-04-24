namespace TDC.Models;
public class Profile
{
    public long UserId { get; set; }
    public string Username { get; set; }
    public string Description { get; set; }
    public Character Character { get; set; }

    #region constructors
    public Profile(long UserId, string Username, string Description, Character character)
    {
        this.UserId = UserId;
        this.Username = Username;
        this.Description = Description;
        Character = character;
    }
    #endregion
}

