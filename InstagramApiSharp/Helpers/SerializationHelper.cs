using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization;

namespace InstagramApiSharp.Helpers
{
    internal class SerializationHelper
    {
        public static string SerializeToStream(object o)
        {
            return JsonConvert.SerializeObject(o);
            //var stream = new MemoryStream();
            //DataContractSerializer serializer = new DataContractSerializer(o.GetType());
            //serializer.WriteObject(stream, o);
            //stream.Position = 0;
            //return stream;
        }

        public static T DeserializeFromStream<T>(string stream)
        {
            return JsonConvert.DeserializeObject<T>(stream);
            //var formatter = new DataContractSerializer(stream.GetType());
            //stream.Seek(0, SeekOrigin.Begin);
            //var fromStream = formatter.ReadObject(stream);
            //return (T)fromStream;
        }
    }
}