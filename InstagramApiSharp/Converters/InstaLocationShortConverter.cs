using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaLocationShortConverter : IObjectConverter<InstaLocationShort, InstaLocationShortResponse>
    {
        public InstaLocationShortResponse SourceObject { get; set; }

        public InstaLocationShort Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var location = new InstaLocationShort
            {
                Name = SourceObject.Name,
                Address = SourceObject.Address,
                ExternalSource = SourceObject.ExternalIdSource,
                ExternalId = SourceObject.ExternalId,
                Lat = SourceObject.Lat,
                Lng = SourceObject.Lng
            };
            return location;
        }
    }
}