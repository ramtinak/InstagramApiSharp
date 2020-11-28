using System;
using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Helpers
{
    internal static class ErrorHandlingHelper
    {
        internal static BadStatusResponse GetBadStatusFromJsonString(string json)
        {
            var badStatus = new BadStatusResponse();
            try
            {
                if (json.Contains("Oops, an error occurred"))
                    badStatus.Message = json;
                else if (json.Contains("debug_info"))
                {
                    JObject root = JObject.Parse(json);
                    JToken debugInfo = root["debug_info"];
                    string type = debugInfo["type"].ToString();
                    string message = debugInfo["message"].ToString();

                    badStatus = new BadStatusResponse() { Message = message, ErrorType = type };
                }

                else badStatus = JsonConvert.DeserializeObject<BadStatusResponse>(json);
            }
            catch (Exception ex)
            {
                badStatus.Message = ex.Message;
            }

            return badStatus;
        }
    }
}