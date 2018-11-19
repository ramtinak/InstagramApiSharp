using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1 || NETSTANDARD2_2 || NETSTANDARD2_3
using System.Runtime.Serialization.Formatters.Binary;
#endif
namespace InstagramApiSharp.Helpers
{
    internal class SerializationHelper
    {
        public static Stream SerializeToStream(object o)
        {
            var stream = new MemoryStream();
#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1 || NETSTANDARD2_2 || NETSTANDARD2_3
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
#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1 || NETSTANDARD2_2 || NETSTANDARD2_3

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