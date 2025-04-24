namespace TDC.Models;

public class Character
{
    public string Color { get; set; }
    public string FaceImage { get; set; }
    public long XP { get; set; }
    public CharacterStats Stats { get; set; }

    #region constructors
    public Character(string color, string face, long xp, CharacterStats stats)
    {
        Color = color;
        FaceImage = face;
        XP = xp;
        Stats = stats;
    }
    #endregion
}
