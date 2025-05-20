namespace TDC.Backend.IDataRepository
{
    public interface IFaceRepository
    {
        public string? GetImageForFaceId(string faceId);
    }
}
