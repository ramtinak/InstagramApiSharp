using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class BadStatusErrorsResponseRecovery : BaseStatusResponse
    {
        [JsonProperty("errors")] public MessageErrorsResponsePhone Phone_number { get; set; }
    }

    public class MessageErrorsResponsePhone
    {
        [JsonProperty("phone_number")] public List<string> Errors { get; set; }
    }

    public class MessageErrorsResponseRecoveryEmail : BaseStatusResponse
    {
        [JsonProperty("message")] public string Message { get; set; }
    }
}
