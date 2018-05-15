namespace InstagramApiSharp.Classes.Models
{
    public class InstaLocation : InstaLocationShort
    {
        public long FacebookPlacesId { get; set; }

        public string City { get; set; }

        public long Pk { get; set; }

        public string ShortName { get; set; }
    }
}