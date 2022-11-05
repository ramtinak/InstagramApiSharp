/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Linq;
using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Converters.Json
{
    internal class InstaFriendShipShortDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstaFriendshipShortStatusListResponse);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var root = JToken.Load(reader);
            var statusSubContainer = root["friendship_statuses"];
            var list = new InstaFriendshipShortStatusListResponse();
            var extras = statusSubContainer.ToObject<InstaExtraResponse>();

            if (extras != null && extras.Extras != null && extras.Extras.Any())
            {
                foreach (var item in extras.Extras)
                {
                    try
                    {
                        var f = item.Value.ToObject<InstaFriendshipShortStatusResponse>();
                        f.Pk = long.Parse(item.Key);
                        list.Add(f);
                    }
                    catch { }
                }
            }
            return list;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
