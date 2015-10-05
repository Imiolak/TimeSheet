using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TimeSheet.Utils
{
    public static class FileSerializer
    {
        public static void Serialize<T>(T obj, string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                new BinaryFormatter().Serialize(stream, obj);
            }
        }

        public static T Deserialize<T>(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return default(T);
            }

            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var formatter = new BinaryFormatter();
                T obj;
                try
                {
                    obj = (T) formatter.Deserialize(stream);
                }
                catch (SerializationException)
                {
                    return default(T);
                }
                
                return obj;
            }
        }
    }
}
