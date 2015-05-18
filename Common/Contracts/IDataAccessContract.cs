using Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts
{
    [ContractClassFor(typeof(IDataAccess))]
    public abstract class IDataAccessContract : IDataAccess
    {
        void IDataAccess.CreateDatabase(Models.Database database)
        {
            Contract.Requires(database != null);
            Contract.Requires(!String.IsNullOrEmpty(database.Name));
            Contract.Requires(database.Tables.Count > 0);
        }

        void IDataAccess.DropDatabase(Models.Database database)
        {
            throw new NotImplementedException();
        }

        void IDataAccess.Insert(string db, string table, IList<Models.Record> records)
        {
            throw new NotImplementedException();
        }

        IList<Models.Record> IDataAccess.Update(string db, string table, IDictionary<string, object> set, IDictionary<string, object> where)
        {
            Contract.Ensures(Contract.Result<IList<Record>>().Count >= 0);
            return default(IList<Record>);
        }

        void IDataAccess.Delete(string db, string table, IDictionary<string, object> where)
        {
            throw new NotImplementedException();
        }

        IList<Models.Record> IDataAccess.Select(string db, IList<string> columns, string table, IDictionary<string, object> where)
        {
            Contract.Requires(!String.IsNullOrEmpty(db));
            Contract.Requires(columns.Count > 0);
            Contract.Requires(!String.IsNullOrEmpty(table));
            Contract.Ensures(Contract.Result<IList<Record>>().Count > 0);
            return default(IList<Record>);
        }
    }
}
