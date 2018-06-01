using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace InstagramApiSharp.Helpers
{
    internal class SerializationHelper
    {
        public static Stream SerializeToStream(object o)
        {
            //var stream = new MemoryStream();
            //DataContractSerializer serializer = new DataContractSerializer(o.GetType());
            //serializer.WriteObject(stream, o);
            //stream.Position = 0;
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, o);
            stream.Position = 0;
            return stream;
        }

        public static T DeserializeFromStream<T>(Stream stream)
        {
            //var formatter = new DataContractSerializer(stream.GetType());
            //stream.Seek(0, SeekOrigin.Begin);
            //var fromStream = formatter.ReadObject(stream);
            //return (T)fromStream;
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
        public static string SerializeToString(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public static T DeserializeFromString<T>(string stream)
        {
            return JsonConvert.DeserializeObject<T>(stream);
        }
    }
}