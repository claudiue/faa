using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IDataAccess
    {
        void CreateDatabase(IDatabase database);
        void DropDatabase(IDatabase database);
        void Insert(string table, IList<IRecord> records);
        void Update(string table, IDictionary<string, object> set, IDictionary<string, object> where);
        void Delete(string table, IDictionary<string, object> where);
        IList<IRecord> Select(IList<string> columns, string table, IDictionary<string, object> where);
    }
}
