using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;

namespace InstagramApiSharp.Converters
{
    internal class InstaCaptionConverter : IObjectConverter<InstaCaption, InstaCaptionResponse>
    {
        public InstaCaptionResponse SourceObject { get; set; }

        public InstaCaption Convert()
        {
            var caption = new InstaCaption
            {
                Pk = SourceObject.Pk,
                CreatedAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject.CreatedAtUnixLike),
                CreatedAtUtc = DateTimeHelper.UnixTimestampToDateTime(SourceObject.CreatedAtUtcUnixLike),
                MediaId = SourceObject.MediaId,
                Text = SourceObject.Text,
                User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.User).Convert(),
                UserId = SourceObject.UserId
            };
            return caption;
        }
    }
}