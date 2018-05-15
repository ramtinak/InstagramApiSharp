namespace InstagramApiSharp.Classes.Models
{
    public class InstaImage
    {
        public InstaImage(string uri, int width, int height)
        {
            URI = uri;
            Width = width;
            Height = height;
        }

        public InstaImage()
        {
        }

        public string URI { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}