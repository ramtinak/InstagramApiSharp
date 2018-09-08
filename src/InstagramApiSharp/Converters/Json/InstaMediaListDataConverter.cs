using System;
using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Converters.Json
{
    internal class InstaMediaListDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstaMediaListResponse);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var root = JToken.Load(reader);
            var feed = root.ToObject<InstaMediaListResponse>();
            feed.Medias.Clear();
            feed.Stories.Clear();
            var items = root.SelectToken("items");
            var storiesTray = root.SelectToken("items[0].stories.tray");
            foreach (var item in items)
            {
                var mediaToken = item?.SelectToken("media");
                var media = mediaToken != null
                    ? mediaToken.ToObject<InstaMediaItemResponse>()
                    : item?.ToObject<InstaMediaItemResponse>();
                if (string.IsNullOrEmpty(media?.Pk)) continue;
                feed.Medias.Add(media);
            }

            if (storiesTray == null) return feed;
            foreach (var storyItem in storiesTray)
            {
                var story = storyItem.ToObject<InstaStoryResponse>();
                if (story == null) continue;
                feed.Stories.Add(story);
            }

            return feed;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}