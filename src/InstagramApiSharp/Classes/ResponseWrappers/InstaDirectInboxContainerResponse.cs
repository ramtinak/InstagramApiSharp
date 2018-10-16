using System.Collections.Generic;
using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaDirectInboxContainerResponse : BaseStatusResponse
    {
        [JsonProperty("pending_requests_total")] public int PendingRequestsCount { get; set; }

        [JsonProperty("seq_id")] public int SeqId { get; set; }

        [JsonProperty("subscription")] public InstaDirectInboxSubscriptionResponse Subscription { get; set; }

        [JsonProperty("inbox")] public InstaDirectInboxResponse Inbox { get; set; }

        [JsonProperty("pending_requests_users")] public List<InstaUserShortResponse> PendingUsers { get; set; }

        [JsonProperty("snapshot_at_ms")] public long? SnapshotAtMs { get; set; }
    }
}