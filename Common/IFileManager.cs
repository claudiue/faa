using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IFileManager
    {
        void CreateFolder(string path);
        void CreateFile(string path, string fileName);
        void WriteLine(string path, string fileName, string line);
        void WriteList(string path, string fileName, IList<Record> list);
        IList<string> ReadFile(string path, string fileName);
    }
}
