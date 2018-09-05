using System;
using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Converters.Json
{
    internal class InstaFriendShipDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstaFriendshipStatusResponse);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var root = JToken.Load(reader);
            var statusSubContainer = root["friendship_status"];
            return statusSubContainer == null
                ? root.ToObject<InstaFriendshipStatusResponse>()
                : statusSubContainer.ToObject<InstaFriendshipStatusResponse>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}