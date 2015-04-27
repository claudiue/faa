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
                File.Create(fileName);
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
            
        }

        public IList<string> ReadFile(string path, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
