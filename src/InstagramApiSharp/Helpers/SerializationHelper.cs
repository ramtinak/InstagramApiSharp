using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization;
using InstagramApiSharp.Classes;
using System;
#if NETFRAMEWORK || NETSTANDARD
using System.Runtime.Serialization.Formatters.Binary;
#else
using System.Text;
#endif
namespace InstagramApiSharp.Helpers
{
    internal class SerializationHelper
    {
        public static Stream SerializeToStream(object o)
        {
            var stream = new MemoryStream();
#if NETFRAMEWORK || NETSTANDARD
            IFormatter formatter = new BinaryFormatter
            {
                Binder = new BinaryFormatterSerializationBinder()
            };
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
#if NETFRAMEWORK || NETSTANDARD
            IFormatter formatter = new BinaryFormatter
            {
                Binder = new BinaryFormatterSerializationBinder()
            };
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
#if NETFRAMEWORK || NETSTANDARD
    class BinaryFormatterSerializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            if (typeName.Equals(typeof(StateData).FullName))
                return typeof(StateData);
            return null;
        }
    }
#endif
}