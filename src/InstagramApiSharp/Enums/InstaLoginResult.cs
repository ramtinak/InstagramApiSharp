namespace InstagramApiSharp.Classes
{
    public enum InstaLoginResult
    {
        Success,
        BadPassword,
        InvalidUser,
        TwoFactorRequired,
        Exception,
        ChallengeRequired,
        LimitError,
        InactiveUser,
        CheckpointLoggedOut
    }
}