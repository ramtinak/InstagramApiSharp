/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaPlaceConverter : IObjectConverter<InstaPlace, InstaPlaceResponse>
    {
        public InstaPlaceResponse SourceObject { get; set; }

        public InstaPlace Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var place = new InstaPlace
            {
                Location = ConvertersFabric.Instance.GetPlaceShortConverter(SourceObject.Location).Convert(),
                Title = SourceObject.Title,
                Subtitle = SourceObject.Subtitle
            };
            return place;
        }
    }
}
