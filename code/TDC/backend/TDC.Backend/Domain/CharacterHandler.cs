using TDC.Backend.IDomain;

namespace TDC.Backend.Domain
{
    public class CharacterHandler: ICharacterHandler
    {
        public string GetDefaultCharacterImage()
        {
            throw new NotImplementedException();
        }
        public string GetCharacterFaceForUser(string username)
        {
            throw new NotImplementedException();
        }
        public string GetCharacterBodyForUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCharacterFace(string username, long faceId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCharacterColor(string username, string color)
        {
            throw new NotImplementedException();
        }
    }
}
