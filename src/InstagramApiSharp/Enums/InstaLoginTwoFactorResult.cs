namespace InstagramApiSharp.Classes
{
    public enum InstaLoginTwoFactorResult
    {
        Success, //Ok
        InvalidCode, //sms_code_validation_code_invalid
        CodeExpired, //invalid_nonce
        Exception,
        ChallengeRequired
    }
}