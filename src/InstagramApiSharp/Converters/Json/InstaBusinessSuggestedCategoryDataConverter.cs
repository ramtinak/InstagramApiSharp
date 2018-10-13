using System;
using System.Collections.Generic;
using InstagramApiSharp.Classes.Models.Business;
using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
namespace InstagramApiSharp.Converters.Json
{
    internal class InstaBusinessSuggestedCategoryDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstaBusinessSuggestedCategoryList);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var container = token.ToObject<InstaBusinessCategoryContainer>();
            var items = container.Extras.FirstOrDefault().Value["categories"];
            var categories = items.ToObject<InstaBusinessSuggestedCategoryList>();
            return categories;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
