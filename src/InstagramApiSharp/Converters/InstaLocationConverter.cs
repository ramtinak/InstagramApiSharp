using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaLocationConverter : IObjectConverter<InstaLocation, InstaLocationResponse>
    {
        public InstaLocationResponse SourceObject { get; set; }

        public InstaLocation Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var location = new InstaLocation
            {
                Name = SourceObject.Name,
                Address = SourceObject.Address,
                City = SourceObject.City,
                ExternalSource = SourceObject.ExternalIdSource,
                ExternalId = SourceObject.ExternalId,
                Lat = SourceObject.Lat,
                Lng = SourceObject.Lng,
                Pk = SourceObject.Pk,
                ShortName = SourceObject.ShortName,
                Height = SourceObject.Height,
                Rotation = SourceObject.Rotation,
                Width = SourceObject.Width,
                X = SourceObject.X,
                Y = SourceObject.Y
            };
            return location;
        }
    }
}