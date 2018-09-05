namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public interface IInstaRecipientsResponse
    {
        long Expires { get; set; }

        bool Filtered { get; set; }

        string RankToken { get; set; }

        string RequestId { get; set; }

        RankedRecipientResponse[] RankedRecipients { get; set; }
    }
}