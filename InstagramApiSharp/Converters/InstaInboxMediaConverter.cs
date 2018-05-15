using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaInboxMediaConverter : IObjectConverter<InstaInboxMedia, InstaInboxMediaResponse>
    {
        public InstaInboxMediaResponse SourceObject { get; set; }

        public InstaInboxMedia Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var inboxMedia = new InstaInboxMedia
            {
                MediaType = SourceObject.MediaType,
                OriginalHeight = SourceObject.OriginalHeight,
                OriginalWidth = SourceObject.OriginalWidth
            };
            if (SourceObject?.ImageCandidates?.Candidates == null) return inboxMedia;
            foreach (var image in SourceObject.ImageCandidates.Candidates)
                inboxMedia.Images.Add(new InstaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));
            return inboxMedia;
        }
    }
}