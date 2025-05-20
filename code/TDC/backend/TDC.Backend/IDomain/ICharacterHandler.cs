namespace TDC.Backend.IDomain
{
    public interface ICharacterHandler
    {
        public string GetDefaultCharacterImage();
        public string GetCharacterFaceForUser(string username);
        public string GetCharacterBodyForUser(string username);
    }
}
