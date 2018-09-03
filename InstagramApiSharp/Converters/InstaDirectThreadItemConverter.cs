using System;
using System.Collections.Generic;
using System.Linq;
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
                    Text = SourceObject.StoryShare.Text,
                    IsLinked = SourceObject.StoryShare.IsLinked,
                    Message = SourceObject.StoryShare.Message,
                    Title = SourceObject.StoryShare.Title
                };
                if (SourceObject.StoryShare.Media != null)
                {
                    var converter = ConvertersFabric.Instance.GetSingleMediaConverter(SourceObject.StoryShare.Media);
                    threadItem.StoryShare.Media = converter.Convert();
                }
            }
            else if (threadItem.ItemType == InstaDirectThreadItemType.Text)
            {
                threadItem.Text = SourceObject.Text;
            } 
            else if (threadItem.ItemType == InstaDirectThreadItemType.RavenMedia &&
                SourceObject.RavenMedia != null)
            {
                var converter = ConvertersFabric.Instance.GetSingleMediaConverter(SourceObject.RavenMedia);
                threadItem.RavenMedia = converter.Convert();
                threadItem.RavenSeenUserIds = SourceObject.RavenSeenUserIds;
                threadItem.RavenViewMode = SourceObject.RavenViewMode;
                threadItem.RavenReplayChainCount = SourceObject.RavenReplayChainCount;
                threadItem.RavenSeenCount = SourceObject.RavenSeenCount;
                if (SourceObject.RavenExpiringMediaActionSummary != null)
                {
                    InstaRavenType ravenType = SourceObject.RavenExpiringMediaActionSummary.Type.ToLower() == "raven_delivered" ? InstaRavenType.Delivered : InstaRavenType.Opened;
                    threadItem.RavenExpiringMediaActionSummary = new InstaRavenMediaActionSummary
                    {
                        Count = SourceObject.RavenExpiringMediaActionSummary.Count,
                        Type = ravenType
                    };
                    if (!string.IsNullOrEmpty(SourceObject.RavenExpiringMediaActionSummary.TimeStamp))
                        threadItem.RavenExpiringMediaActionSummary.
                            ExpireTime = DateTimeHelper.UnixTimestampMilisecondsToDateTime(SourceObject.RavenExpiringMediaActionSummary.TimeStamp);

                }
            }
            else if (threadItem.ItemType == InstaDirectThreadItemType.ActionLog && SourceObject.ActionLogMedia != null)
            {
                threadItem.ActionLogMedia = new InstaActionLog
                {
                    Description = SourceObject.ActionLogMedia.Description
                };
            }
            else if (threadItem.ItemType == InstaDirectThreadItemType.Profile && SourceObject.ProfileMedia != null)
            {
                var converter = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.ProfileMedia);
                threadItem.ProfileMedia = converter.Convert(); 
                if(SourceObject.ProfileMediasPreview != null && SourceObject.ProfileMediasPreview.Any())
                {
                    try
                    {
                        var previewMedias = new List<InstaMedia>();
                        foreach(var item in SourceObject.ProfileMediasPreview)
                            previewMedias.Add(ConvertersFabric.Instance.GetSingleMediaConverter(item).Convert());

                        threadItem.ProfileMediasPreview = previewMedias;
                    }
                    catch { }
                }
            }
            else if (threadItem.ItemType == InstaDirectThreadItemType.Placeholder && SourceObject.Placeholder != null)
            {
                threadItem.Placeholder = new InstaPlaceholder
                {
                    IsLinked = SourceObject.Placeholder.IsLinked,
                    Message = SourceObject.Placeholder.Message
                };
            }
            return threadItem;
        }
    }
}