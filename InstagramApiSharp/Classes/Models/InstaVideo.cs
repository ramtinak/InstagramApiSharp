namespace InstagramApiSharp.Classes.Models
{
    public class InstaVideo
    {
        public InstaVideo(string url, int width, int height) : this(url, width, height, 3) { }
        public InstaVideo(string url, int width, int height, int type)
        {
            Url = url;
            Width = width;
            Height = height;
            Type = type;
        }

        public string Url { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Type { get; set; }

        internal string UploadId { get; set; }

        public double Length { get; set; } = 0;
    }
}