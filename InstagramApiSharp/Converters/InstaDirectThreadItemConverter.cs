using System;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;

namespace InstagramApiSharp.Converters
{
    internal class InstaDirectThreadItemConverter : IObjectConverter<InstaDirectInboxItem, InstaDirectInboxItemResponse>
    {
        public InstaDirectInboxItemResponse SourceObject { get; set; }

        public InstaDirectInboxItem Convert()
        {
            var threadItem = new InstaDirectInboxItem
            {
                ClientContext = SourceObject.ClientContext,
                ItemId = SourceObject.ItemId
            };

            threadItem.TimeStamp = DateTimeHelper.UnixTimestampMilisecondsToDateTime(SourceObject.TimeStamp);
            threadItem.UserId = SourceObject.UserId;

            var truncatedItemType = SourceObject.ItemType.Trim().Replace("_", "");
            if (Enum.TryParse(truncatedItemType, true, out InstaDirectThreadItemType type))
                threadItem.ItemType = type;

            if (threadItem.ItemType == InstaDirectThreadItemType.Link)
            {
                threadItem.Text = SourceObject.Link?.LinkContext?.LinkUrl;
            }
            else if (threadItem.ItemType == InstaDirectThreadItemType.Like)
            {
                threadItem.Text = SourceObject.Like;
            }
            else if (threadItem.ItemType == InstaDirectThreadItemType.Media
                     && SourceObject.Media != null)
            {
                var converter = ConvertersFabric.Instance.GetInboxMediaConverter(SourceObject.Media);
                threadItem.Media = converter.Convert();
            }
            else if (threadItem.ItemType == InstaDirectThreadItemType.MediaShare
                     && SourceObject.MediaShare != null)
            {
                var converter = ConvertersFabric.Instance.GetSingleMediaConverter(SourceObject.MediaShare);
                threadItem.MediaShare = converter.Convert();
            }
            else if (threadItem.ItemType == InstaDirectThreadItemType.StoryShare
              && SourceObject.StoryShare != null)
            {
                threadItem.StoryShare = new InstaStoryShare
                {
                    IsReelPersisted = SourceObject.StoryShare.IsReelPersisted,
                    ReelType = SourceObject.StoryShare.ReelType,
                    Text = SourceObject.StoryShare.Text
                };
                var converter = ConvertersFabric.Instance.GetSingleMediaConverter(SourceObject.StoryShare.Media);
                threadItem.StoryShare.Media = converter.Convert();
            }
            else if (threadItem.ItemType == InstaDirectThreadItemType.Text)
            {
                threadItem.Text = SourceObject.Text;
            }
            else if (threadItem.ItemType == InstaDirectThreadItemType.RavenMedia &&
                SourceObject.RavenMedia != null)
            {
                threadItem.RavenMedia = new InstaRavenMedia
                {
                    MediaType =  SourceObject.RavenMedia.MediaType
                };
                threadItem.RavenSeenUserIds = SourceObject.RavenSeenUserIds;
                threadItem.RavenViewMode = SourceObject.RavenViewMode;
                threadItem.RavenReplayChainCount = SourceObject.RavenReplayChainCount;
                threadItem.RavenSeenCount = SourceObject.RavenSeenCount;
                InstaRavenType ravenType = SourceObject.RavenExpiringMediaActionSummary.Type.ToLower() == "raven_delivered" ? InstaRavenType.Delivered : InstaRavenType.Opened;
                threadItem.RavenExpiringMediaActionSummary = new InstaRavenMediaActionSummary
                {
                    Count = SourceObject.RavenExpiringMediaActionSummary.Count,
                    ExpireTime = DateTimeHelper.UnixTimestampMilisecondsToDateTime(SourceObject.RavenExpiringMediaActionSummary.TimeStamp),
                    Type = ravenType
                };
            }
            return threadItem;
        }
    }
}