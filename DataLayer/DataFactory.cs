using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataFactory
    {
        public IDataAccess CreateDataAccess()
        {
            Contract.Ensures(Contract.Result<IDataAccess>() != null);
            return new DataAccess(new FileManager());
        }

        public IDataAccess CreateDataAccess(IFileManager fileManager)
        {
            Contract.Requires(fileManager != null);
            Contract.Ensures(Contract.Result<IDataAccess>() != null);
            return new DataAccess(fileManager);
        }

        public IFileManager CreateFileManager() 
        {
            Contract.Ensures(Contract.Result<IFileManager>() != null);
            return new FileManager();
        }
    }
}
