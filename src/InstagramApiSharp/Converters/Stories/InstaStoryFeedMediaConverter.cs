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
    internal class InstaStoryFeedMediaConverter : IObjectConverter<InstaStoryFeedMedia, InstaStoryFeedMediaResponse>
    {
        public InstaStoryFeedMediaResponse SourceObject { get; set; }

        public InstaStoryFeedMedia Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var storyFeed = new InstaStoryFeedMedia
            {
                Height = SourceObject.Height,
                IsPinned = SourceObject.IsPinned,
                MediaId = SourceObject.MediaId,
                ProductType = SourceObject.ProductType,
                Rotation = SourceObject.Rotation,
                Width = SourceObject.Width,
                X = SourceObject.X,
                Y = SourceObject.Y,
                Z = SourceObject.Z
            };
            return storyFeed;
        }
    }
}
