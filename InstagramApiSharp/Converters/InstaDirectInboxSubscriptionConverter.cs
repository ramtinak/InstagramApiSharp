using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaDirectInboxSubscriptionConverter :
        IObjectConverter<InstaDirectInboxSubscription, InstaDirectInboxSubscriptionResponse>
    {
        public InstaDirectInboxSubscriptionResponse SourceObject { get; set; }

        public InstaDirectInboxSubscription Convert()
        {
            var subscription = new InstaDirectInboxSubscription
            {
                Auth = SourceObject.Auth,
                Sequence = SourceObject.Sequence,
                Topic = SourceObject.Topic,
                Url = SourceObject.Url
            };
            return subscription;
        }
    }
}