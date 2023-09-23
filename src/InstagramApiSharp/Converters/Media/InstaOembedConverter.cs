using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaOembedConverter : IObjectConverter<InstaOembed, InstaOembedUrlResponse>
    {

        public InstaOembedUrlResponse SourceObject { get; set; }

        public InstaOembed Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            return new InstaOembed
            {
                AuthorId = SourceObject.AuthorId,
                AuthorName = SourceObject.AuthorName,
                AuthorUrl = SourceObject.AuthorUrl,
                Height = SourceObject.Height,
                Html = SourceObject.Html,
                MediaId = SourceObject.MediaId,
                ProviderName = SourceObject.ProviderName,
                ProviderUrl = SourceObject.ProviderUrl,
                ThumbnailHeight = SourceObject.ThumbnailHeight,
                ThumbnailUrl = SourceObject.ThumbnailUrl,
                ThumbnailWidth = SourceObject.ThumbnailWidth,
                Title = SourceObject.Title,
                Type = SourceObject.Type,
                Version = SourceObject.Version,
                Width = SourceObject.Width
            };
        }
    }
}
