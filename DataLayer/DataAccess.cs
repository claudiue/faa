using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataAccess : IDataAccess
    {

        public void CreateDatabase(IDatabase database)
        {
            throw new NotImplementedException();
        }

        public void DropDatabase(IDatabase database)
        {
            throw new NotImplementedException();
        }

        public void Insert(string table, IList<IRecord> records)
        {
            throw new NotImplementedException();
        }

        public void Update(string table, IDictionary<string, object> set, IDictionary<string, object> where)
        {
            throw new NotImplementedException();
        }

        public void Delete(string table, IDictionary<string, object> where)
        {
            throw new NotImplementedException();
        }

        public IList<IRecord> Select(IList<string> columns, string table, IDictionary<string, object> where)
        {
            throw new NotImplementedException();
        }
    }
}
