namespace InstagramApiSharp.Classes
{
    public enum ResponseType
    {
        /// <summary>
        ///     Unknown type:|
        /// </summary>
        Unknown = 0,
        /// <summary>
        ///     When happens that your account needs re-login
        /// </summary>
        LoginRequired = 1,
        /// <summary>
        ///     Unknown behavior:|
        /// </summary>
        CheckPointRequired = 2,
        /// <summary>
        ///     Your requests is limited because you are spamming Instagram
        /// </summary>
        RequestsLimit = 3,
        /// <summary>
        ///     Your account is banned from API
        /// </summary>
        SentryBlock = 4,
        /// <summary>
        ///     Everything works fine
        /// </summary>
        OK = 5,
        /// <summary>
        ///     Wrong request
        /// </summary>
        WrongRequest = 6,
        /// <summary>
        ///     Some pages skipped from pagination:|
        /// </summary>
        SomePagesSkipped = 7,
        /// <summary>
        ///     Unexpected response happens
        /// </summary>
        UnExpectedResponse = 8,
        /// <summary>
        ///     Internal exception thrown
        /// </summary>
        InternalException = 9,
        /// <summary>
        ///     When happens that instagram suspicious of you and you need to prove that you aren't bot/spammer
        /// </summary>
        ChallengeRequired = 10,
        /// <summary>
        ///     When happens that you created an account without submitting phone number or email
        /// </summary>
        InactiveUser = 11,
        /// <summary>
        ///     When happens that you didn't accept consent age in GDPR countries
        /// </summary>
        ConsentRequired = 12,
        /// <summary>
        ///     When happens that you are doing massive follow and unfollow, comment and...
        /// </summary>
        ActionBlocked = 13
    }
}