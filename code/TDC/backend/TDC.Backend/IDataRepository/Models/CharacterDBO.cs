namespace TDC.Backend.IDataRepository.Models
{
    public class CharacterDbo
    {
        public string Username { get; set; }
        public string FaceId { get; set; }
        public string Color { get; set; }
        public long XP { get; set; }

        public CharacterDbo() {}

        public CharacterDbo(string username, string faceId, string color, long xP)
        {
            Username = username;
            FaceId = faceId;
            Color = color;
            XP = xP;
        }
    }
}
