using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaUserTagConverter : IObjectConverter<InstaUserTag, InstaUserTagResponse>
    {
        public InstaUserTagResponse SourceObject { get; set; }

        public InstaUserTag Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var userTag = new InstaUserTag();
            if (SourceObject.Position?.Length == 2)
                userTag.Position = new InstaPosition(SourceObject.Position[0], SourceObject.Position[1]);
            userTag.TimeInVideo = SourceObject.TimeInVideo;
            if (SourceObject.User != null)
                userTag.User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.User).Convert();
            return userTag;
        }
    }
}