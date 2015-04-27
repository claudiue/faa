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
    }
}
