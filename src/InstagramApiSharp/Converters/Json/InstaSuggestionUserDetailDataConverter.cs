using System;
using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Converters.Json
{
    internal class InstaSuggestionUserDetailDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstaSuggestionItemListResponse);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var items = token.SelectToken("items");
            var suggest = items.ToObject<InstaSuggestionItemListResponse>();

            return suggest;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
