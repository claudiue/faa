using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataFactory
    {
        public IDataAccess CreateDataAccess()
        {
            return new DataAccess(new FileManager());
        }

        public IDataAccess CreateDataAccess(IFileManager fileManager)
        {
            return new DataAccess(fileManager);
        }

        public IFileManager CreateFileManager() 
        {
            return new FileManager();
        }
    }
}
