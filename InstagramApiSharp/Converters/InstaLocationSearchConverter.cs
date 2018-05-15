using System;
using System.Linq;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaLocationSearchConverter : IObjectConverter<InstaLocationShortList, InstaLocationSearchResponse>
    {
        public InstaLocationSearchResponse SourceObject { get; set; }

        public InstaLocationShortList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var locations = new InstaLocationShortList();
            locations.AddRange(SourceObject.Locations.Select(location =>
                ConvertersFabric.Instance.GetLocationShortConverter(location).Convert()));
            return locations;
        }
    }
}