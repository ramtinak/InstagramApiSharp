using System;
using System.Collections.Generic;
using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Converters.Json
{
    internal class InstaCollectionDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstaMediaListResponse);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var root = JToken.Load(reader);
            var feed = root.ToObject<List<InstaCollectionItemsToMedia>>();

            var listMedia = new InstaMediaListResponse();
            feed.ForEach(item => listMedia.Medias.Add(item.Media));

            return listMedia;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        private class InstaCollectionItemsToMedia
        {
            [JsonProperty("media")] public InstaMediaItemResponse Media { get; set; }
        }
    }
}