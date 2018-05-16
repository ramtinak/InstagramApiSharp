using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization;

namespace InstagramApiSharp.Helpers
{
    internal class SerializationHelper
    {
        public static Stream SerializeToStream(object o)
        {
            var stream = new MemoryStream();
            DataContractSerializer serializer = new DataContractSerializer(o.GetType());
            serializer.WriteObject(stream, o);
            stream.Position = 0;
            return stream;
        }

        public static T DeserializeFromStream<T>(Stream stream)
        {
            var formatter = new DataContractSerializer(stream.GetType());
            stream.Seek(0, SeekOrigin.Begin);
            var fromStream = formatter.ReadObject(stream);
            return (T)fromStream;
        }
    }
}