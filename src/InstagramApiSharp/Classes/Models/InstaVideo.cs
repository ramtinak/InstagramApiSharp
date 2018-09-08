using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.Models
{
    public class InstaVideo
    {
        public InstaVideo() { }
        public InstaVideo(string url, int width, int height) : this(url, width, height, 3) { }
        public InstaVideo(string url, int width, int height, int type)
        {
            Uri = url;
            Width = width;
            Height = height;
            Type = type;
        }

        public string Uri { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Type { get; set; }

        internal string UploadId { get; set; }

        public double Length { get; set; } = 0;

        [JsonIgnore]
        /// <summary>
        /// This is only for .NET core apps like UWP(Windows 10) apps
        /// </summary>
        public byte[] VideoBytes { get; set; }
    }
}