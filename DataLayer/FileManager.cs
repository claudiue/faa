using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class FileManager : IFileManager
    {
        internal FileManager() { }

        public void CreateFolder(string path)
        {
            path = String.Format("{0}/{1}", Config.Path, path);
            if (!Directory.Exists(path)) 
            {
                Directory.CreateDirectory(path);
            }
        }

        public void CreateFile(string path, string fileName)
        {
            fileName = String.Format("{0}/{1}/{2}.csv", Config.Path, path, fileName);
            if (!File.Exists(fileName)) 
            {
                var fs = File.Create(fileName);
                fs.Close();
            }
        }

        public void WriteLine(string path, string fileName, string line)
        {
            fileName = String.Format("{0}/{1}/{2}.csv", Config.Path, path, fileName);

            using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(line);
                sw.Close();
            }
        }


        public void WriteList(string path, string fileName, IList<Record> list)
        {
            fileName = String.Format("{0}/{1}/{2}.csv", Config.Path, path, fileName);

            var fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            var sw = new StreamWriter(fs);
            foreach (Record r in list)
            {
                sw.WriteLine(r.Values);
            }
            sw.Close();
        }

        public IList<string> ReadFile(string path, string fileName)
        {
            return new List<string>
            {
                "id:System.Int32,name:System.String,age:System.Int32",
                "1,Claudiu,24",
                "2,Andreea,23",
                "3,Vasile,43",
                "4,Ionut,35",
                "5,Andreea,38",
                "6,Claudiu,8",
                "7,Mihai,24"
            };
        }


        public void DeleteFolder(string path)
        {
            path = String.Format("{0}/{1}", Config.Path, path);
            Directory.Delete(path, true);
        }
    }
}
