using Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts
{
    [ContractClassFor(typeof(IFileManager))]
    public abstract class IFileManagerContract : IFileManager
    {
        void IFileManager.CreateFolder(string path)
        {
            Contract.Requires(path != null);
            Contract.Requires(!String.IsNullOrEmpty(path));
        }

        void IFileManager.DeleteFolder(string path)
        {
            throw new NotImplementedException();
        }

        void IFileManager.CreateFile(string path, string fileName)
        {
            Contract.Requires(!String.IsNullOrEmpty(path));
            Contract.Requires(!String.IsNullOrEmpty(fileName));
        }

        void IFileManager.WriteLine(string path, string fileName, string line)
        {
            throw new NotImplementedException();
        }

        void IFileManager.WriteList(string path, string fileName, IList<Record> list)
        {
            Contract.Requires(!String.IsNullOrEmpty(path));
            Contract.Requires(!String.IsNullOrEmpty(fileName));
            Contract.Requires(list.Count > 0);
        }

        IList<string> IFileManager.ReadFile(string path, string fileName)
        {
            Contract.Ensures(Contract.Result<IList<string>>().Count > 0);
            return default(IList<string>);
        }
    }
}
