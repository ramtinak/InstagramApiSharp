using System;
using System.Collections.Generic;
using System.Linq;
using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Converters.Json
{
    internal class InstaTagFeedDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstaTagFeedResponse);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var root = JToken.Load(reader);
            var feed = root.ToObject<InstaTagFeedResponse>();
            feed.Medias.Clear();
            feed.RankedItems.Clear();
            feed.Stories.Clear();
            var story = root.SelectToken("story");
            var rankedItems = root.SelectToken("ranked_items");
            var items = root.SelectToken("items");
            var storiesTray = root.SelectToken("story.items");

            List<InstaMediaItemResponse> GetMedias(JToken token)
            {
                return token.Select(item => item?.ToObject<InstaMediaItemResponse>())
                    .Where(media => !string.IsNullOrEmpty(media?.Pk)).ToList();
            }

            if (items != null)
                feed.Medias.AddRange(GetMedias(items));
            if (rankedItems != null)
                feed.RankedItems.AddRange(GetMedias(rankedItems));
            if (storiesTray == null) return feed;
            foreach (var storyItem in storiesTray)
            {
                var storyTrayIem = storyItem.ToObject<InstaStoryResponse>();
                if (story == null) continue;
                feed.Stories.Add(storyTrayIem);
            }

            return feed;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}