namespace TDC.Backend.IDataRepository.Models
{
    public class CharacterBodyDbo
    {
        public string Color { get; set; }
        public string Image { get; set; }

        public CharacterBodyDbo() {}

        public CharacterBodyDbo(string color, string image)
        {
            Color = color;
            Image = image;
        }
    }
}
