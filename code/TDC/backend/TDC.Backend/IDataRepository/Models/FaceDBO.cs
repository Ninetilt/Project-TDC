namespace TDC.Backend.IDataRepository.Models
{
    public class FaceDbo
    {
        public long Id { get; set; }
        public string Image { get; set; }

        public FaceDbo() { }

        public FaceDbo(long id, string image)
        {
            Id = id;
            Image = image;
        }
    }
}
