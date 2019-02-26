/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using System;

namespace InstagramApiSharp.Converters
{
    internal class InstaStoryLocationConverter : IObjectConverter<InstaStoryLocation, InstaStoryLocationResponse>
    {
        public InstaStoryLocationResponse SourceObject { get; set; }

        public InstaStoryLocation Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var storyLocation = new InstaStoryLocation
            {
                Height = SourceObject.Height,
                IsHidden = SourceObject.IsHidden,
                IsPinned = SourceObject.IsPinned,
                Rotation = SourceObject.Rotation,
                Width = SourceObject.Width,
                X = SourceObject.X,
                Y = SourceObject.Y,
                Z = SourceObject.Z,
                Location = ConvertersFabric.Instance.GetPlaceShortConverter(SourceObject.Location).Convert()
            };

            return storyLocation;
        }
    }
}
