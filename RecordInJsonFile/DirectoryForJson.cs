using System.IO;
using System.Text.Json;

namespace RecordInJsonFile
{
    public class DirectoryForJson<T> where T : class
    {
        private readonly T user;
        private readonly string way;
        private readonly bool appened;

        private readonly DirectoryInfo dirInfo;

        public DirectoryForJson(string way, T user, bool appened)
        {
            this.user = user;
            this.way = way;
            this.appened = appened;

            dirInfo = new DirectoryInfo(way);         
        }

        public void WriteInDerictory()
        {
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            WriteJson(dirInfo.FullName + "\\" + way, user, appened);
        }

        public T ReadFromDerictory()
        {
           return ReadJson(dirInfo.FullName + "\\" + way);
        }

        public static T ReadJson(string way)
        {
            using (StreamReader fs = new StreamReader(way))
            {
                return JsonSerializer.Deserialize<T>(fs.ReadToEnd());
            }            
        }

        public static void WriteJson(string name, T obj, bool append)
        {
            using (StreamWriter sw = new StreamWriter(name + ".json", append))
            {
                sw.Write(JsonSerializer.Serialize(obj));
            }
        }
    }
}
