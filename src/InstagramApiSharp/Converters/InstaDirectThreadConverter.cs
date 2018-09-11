using System.Collections.Generic;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;
using System.Linq;
using Newtonsoft.Json;
using System;
namespace InstagramApiSharp.Converters
{
    internal class InstaDirectThreadConverter : IObjectConverter<InstaDirectInboxThread, InstaDirectInboxThreadResponse>
    {
        public InstaDirectInboxThreadResponse SourceObject { get; set; }

        public InstaDirectInboxThread Convert()
        {
            var thread = new InstaDirectInboxThread
            {
                Canonical = SourceObject.Canonical,
                HasNewer = SourceObject.HasNewer,
                HasOlder = SourceObject.HasOlder,
                IsSpam = SourceObject.IsSpam,
                Muted = SourceObject.Muted,
                Named = SourceObject.Named,
                Pending = SourceObject.Pending,
                VieweId = SourceObject.VieweId,
                LastActivity = DateTimeHelper.UnixTimestampMilisecondsToDateTime(SourceObject.LastActivity),
                ThreadId = SourceObject.ThreadId,
                OldestCursor = SourceObject.OldestCursor,
                IsGroup = SourceObject.IsGroup,
                IsPin = SourceObject.IsPin,
                ValuedRequest = SourceObject.ValuedRequest,
                PendingScore = SourceObject.PendingScore ?? 0,
                VCMuted = SourceObject.VCMuted,
                ReshareReceiveCount = SourceObject.ReshareReceiveCount,
                ReshareSendCount = SourceObject.ReshareSendCount,
                ExpiringMediaReceiveCount = SourceObject.ExpiringMediaReceiveCount,
                ExpiringMediaSendCount = SourceObject.ExpiringMediaSendCount
            };
            //thread.OldestCursor = thread.OldestCursor;
            thread.ThreadType = SourceObject.ThreadType;
            thread.Title = SourceObject.Title;
            if (SourceObject.Inviter != null)
            {
                var userConverter = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.Inviter);
                thread.Inviter = userConverter.Convert();
            }

            if (SourceObject.Items != null && SourceObject.Items.Count > 0)
            {
                thread.Items = new List<InstaDirectInboxItem>();
                foreach (var item in SourceObject.Items)
                {
                    var converter = ConvertersFabric.Instance.GetDirectThreadItemConverter(item);
                    thread.Items.Add(converter.Convert());
                }
            }

            if (SourceObject.Users != null && SourceObject.Users.Count > 0)
            {
                thread.Users = new InstaUserShortList();
                foreach (var user in SourceObject.Users)
                {
                    var converter = ConvertersFabric.Instance.GetUserShortConverter(user);
                    thread.Users.Add(converter.Convert());
                }
            }

            if (SourceObject.LastSeenAt != null && SourceObject.LastSeenAt != null)
            {
                try
                {
                    var lastSeenJson = System.Convert.ToString(SourceObject.LastSeenAt);
                    var obj = JsonConvert.DeserializeObject<InstaLastSeenAtResponse>(lastSeenJson);
                    thread.LastSeenAt = new List<InstaLastSeen>();
                    foreach (var extraItem in obj.Extras)
                    {
                        var convertedLastSeen = JsonConvert.DeserializeObject<InstaLastSeenItemResponse>(extraItem.Value.ToString(Formatting.None));
                        var lastSeen = new InstaLastSeen
                        {
                            PK = long.Parse(extraItem.Key),
                            ItemId = convertedLastSeen.ItemId,
                        };
                        if (convertedLastSeen.TimestampPrivate != null)
                            lastSeen.SeenTime = DateTimeHelper.UnixTimestampMilisecondsToDateTime(convertedLastSeen.TimestampPrivate);
                        thread.LastSeenAt.Add(lastSeen);
                    }
                }
                catch { }
            }


            return thread;
        }
    }
}