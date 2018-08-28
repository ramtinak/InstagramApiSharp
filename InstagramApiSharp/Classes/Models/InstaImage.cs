using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.Models
{
    public class InstaImage
    {
        public InstaImage(string uri, int width, int height)
        {
            Uri = uri;
            Width = width;
            Height = height;
        }

        public InstaImage()
        {
        }

        public string Uri { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        [JsonIgnore]
        /// <summary>
        /// This is only for .NET core apps like UWP(Windows 10) apps
        /// </summary>
        public byte[] ImageBytes { get; set; }
    }
}