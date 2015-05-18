using Common.Contracts;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ContractClass(typeof(IFileManagerContract))]
    public interface IFileManager
    {
        void CreateFolder(string path);
		void DeleteFolder(string path);
        void CreateFile(string path, string fileName);
        void WriteLine(string path, string fileName, string line);
        void WriteList(string path, string fileName, IList<Record> list);
        IList<string> ReadFile(string path, string fileName);
    }
}
