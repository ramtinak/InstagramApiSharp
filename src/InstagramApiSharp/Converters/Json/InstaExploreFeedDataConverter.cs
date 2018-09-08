using System;
using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Converters.Json
{
    internal class InstaExploreFeedDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstaExploreFeedResponse);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var root = JToken.Load(reader);
            var items = root["items"];
            var feed = root.ToObject<InstaExploreFeedResponse>();
            foreach (var item in items)
            {
                var storiesToken = item["stories"];
                var channelToken = item["channel"];
                var mediaToken = item["media"];
                if (storiesToken != null)
                {
                    var storyTray = storiesToken.ToObject<InstaStoryTrayResponse>();
                    feed.Items.StoryTray = storyTray;
                    continue;
                }

                if (channelToken != null)
                {
                    var channel = channelToken.ToObject<InstaChannelResponse>();
                    feed.Items.Channel = channel;
                    continue;
                }

                if (mediaToken != null)
                {
                    var media = mediaToken.ToObject<InstaMediaItemResponse>();
                    feed.Items.Medias.Add(media);
                }
            }

            return feed;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}