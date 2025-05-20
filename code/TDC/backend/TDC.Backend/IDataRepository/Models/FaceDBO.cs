namespace TDC.Backend.IDataRepository.Models
{
    public class FaceDbo
    {
        public string Id { get; set; }
        public string Image { get; set; }

        public FaceDbo() { }

        public FaceDbo(string id, string image)
        {
            Id = id;
            Image = image;
        }
    }
}
