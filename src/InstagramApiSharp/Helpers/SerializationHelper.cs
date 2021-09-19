using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
#if !WINDOWS_UWP
using System.Runtime.Serialization.Formatters.Binary;
#endif
namespace InstagramApiSharp.Helpers
{
    internal class SerializationHelper
    {
        public static Stream SerializeToStream(object o)
        {
            var stream = new MemoryStream();
#if !WINDOWS_UWP
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, o);
            stream.Position = 0;
#else
            var json = SerializeToString(o);
            var bytes = Encoding.UTF8.GetBytes(json);
            stream = new MemoryStream(bytes);
#endif
            return stream;
        }

        public static T DeserializeFromStream<T>(Stream stream)
        {
#if !WINDOWS_UWP
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
#else
            var json = new StreamReader(stream).ReadToEnd();
            return DeserializeFromString<T>(json);
#endif
        }
        public static string SerializeToString(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public static T DeserializeFromString<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}