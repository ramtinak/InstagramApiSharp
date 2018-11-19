using System;
using System.Collections.Generic;
using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Converters.Json
{
    internal class InstaRecentActivityConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstaRecentActivityResponse);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var recentActivity = new InstaRecentActivityResponse();
            if (token.SelectToken("stories") != null)
            {
                recentActivity = token.ToObject<InstaRecentActivityResponse>();
                recentActivity.IsOwnActivity = false;
            }
            else
            {
                if (token.SelectToken("new_stories") != null)
                {
                    var newStories = token.SelectToken("new_stories")?.ToObject<List<InstaRecentActivityFeedResponse>>();
                    recentActivity.Stories.AddRange(newStories ?? throw new InvalidOperationException());
                }
                if (token.SelectToken("old_stories") != null)
                {
                    var oldStories = token.SelectToken("old_stories")?.ToObject<List<InstaRecentActivityFeedResponse>>();
                    recentActivity.Stories.AddRange(oldStories ?? throw new InvalidOperationException());
                }
                recentActivity.IsOwnActivity = true;
            }

            return recentActivity;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}