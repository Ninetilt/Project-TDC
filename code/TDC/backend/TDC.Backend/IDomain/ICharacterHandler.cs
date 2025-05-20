namespace TDC.Backend.IDomain
{
    public interface ICharacterHandler
    {
        public string GetDefaultCharacterImage();
        public string GetCharacterFaceForUser(string username);
        public string GetCharacterBodyForUser(string username);
        public Task UpdateCharacterFace(string username, long faceId);
        public Task UpdateCharacterColor(string username, string color);
    }
}
