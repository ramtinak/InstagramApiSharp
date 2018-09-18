namespace InstagramApiSharp.Classes
{
    public enum ResponseType
    {
        Unknown = 0,
        LoginRequired = 1,
        CheckPointRequired = 2,
        RequestsLimit = 3,
        SentryBlock = 4,
        OK = 5,
        WrongRequest = 6,
        SomePagesSkipped = 7,
        UnExpectedResponse = 8,
        InternalException = 9,
        ChallengeRequired = 10,
        InactiveUser = 11
    }
}