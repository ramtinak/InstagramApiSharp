using System;
using System.Collections.Generic;
using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Converters.Json
{
    internal class InstaFeedResponseDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstaFeedResponse);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var feed = token.ToObject<InstaFeedResponse>();
            var items = token["feed_items"];
            if (items != null)
            {
                foreach (var item in items)
                {
                    if (item["media_or_ad"] != null)
                    {
                        var mediaOrAd = item["media_or_ad"];
                        if (mediaOrAd == null) continue;
                        var media = mediaOrAd.ToObject<InstaMediaItemResponse>();
                        feed.Items.Add(media);
                    }
                    if (item["suggested_users"] != null)
                    {
                        var users = item["suggested_users"]?["suggestions"];
                        if (users != null)
                            foreach (var user in users)
                            {
                                if (user == null) continue;
                                var usr = user.ToObject<InstaSuggestionItemResponse>();
                                feed.SuggestedUsers.Add(usr);
                            }
                    }
                }
            }
            else
            {
                items = token["items"];
                feed.Items = items.ToObject<List<InstaMediaItemResponse>>();
            }
            return feed;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}