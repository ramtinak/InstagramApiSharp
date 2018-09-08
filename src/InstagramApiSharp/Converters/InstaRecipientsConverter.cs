using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaRecipientsConverter : IObjectConverter<InstaRecipients, IInstaRecipientsResponse>
    {
        public IInstaRecipientsResponse SourceObject { get; set; }

        public InstaRecipients Convert()
        {
            var recipients = new InstaRecipients
            {
                ExpiresIn = SourceObject.Expires,
                Filtered = SourceObject.Filtered,
                RankToken = SourceObject.RankToken,
                RequestId = SourceObject.RequestId
            };
            if (SourceObject?.RankedRecipients?.Length > 0)
                foreach (var recipient in SourceObject.RankedRecipients)
                {
                    if (recipient == null) continue;

                    if (recipient.Thread != null)
                    {
                        var rankedThread = new InstaRankedRecipientThread
                        {
                            Canonical = recipient.Thread.Canonical,
                            Named = recipient.Thread.Named,
                            Pending = recipient.Thread.Pending,
                            ThreadId = recipient.Thread.ThreadId,
                            ThreadTitle = recipient.Thread.ThreadTitle,
                            ThreadType = recipient.Thread.ThreadType,
                            ViewerId = recipient.Thread.ViewerId
                        };
                        foreach (var user in recipient.Thread.Users)
                            rankedThread.Users.Add(ConvertersFabric.Instance.GetUserShortConverter(user).Convert());
                        recipients.Threads.Add(rankedThread);
                    }

                    if (recipient.User != null)
                    {
                        var user = ConvertersFabric.Instance.GetUserShortConverter(recipient.User).Convert();
                        recipients.Users.Add(user);
                    }
                }

            return recipients;
        }
    }
}