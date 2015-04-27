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
        void CreateDatabase(Database database);
        void DropDatabase(Database database);
        void Insert(string table, IList<Record> records);
        void Update(string table, IDictionary<string, object> set, IDictionary<string, object> where);
        void Delete(string table, IDictionary<string, object> where);
        IList<Record> Select(IList<string> columns, string table, IDictionary<string, object> where);
    }
}
