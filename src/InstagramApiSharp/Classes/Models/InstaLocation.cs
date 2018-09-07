namespace InstagramApiSharp.Classes.Models
{
    public class InstaLocation : InstaLocationShort
    {
        public double Rotation { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public long FacebookPlacesId { get; set; }

        public string City { get; set; }

        public long Pk { get; set; }

        public string ShortName { get; set; }
    }
}