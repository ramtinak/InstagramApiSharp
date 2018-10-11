using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaHashtagConverter : IObjectConverter<InstaHashtag, InstaHashtagResponse>
    {
        public InstaHashtagResponse SourceObject { get; set; }

        public InstaHashtag Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var hashtag = new InstaHashtag
            {
                Id = SourceObject.Id,
                Name = SourceObject.Name,
                MediaCount = SourceObject.MediaCount,
                ProfilePicUrl = SourceObject.ProfilePicUrl
            };
            return hashtag;
        }
    }
}