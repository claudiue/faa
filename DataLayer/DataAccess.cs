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
        private IFileManager _fileManager;

        internal DataAccess(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public void CreateDatabase(Database database)
        {
            _fileManager.CreateFolder(database.Name);
        }

        public void DropDatabase(Database database)
        {
            throw new NotImplementedException();
        }

        public void Insert(string table, IList<Record> records)
        {
            throw new NotImplementedException();
        }

        public void Update(string table, IDictionary<string, object> set, IDictionary<string, object> where)
        {
            throw new NotImplementedException();
        }

        public void Delete(string table, IDictionary<string, object> where)
        {
            Select(
                columns: new List<string> { "id", "name", "age" },
                table: "students",
                where: new Dictionary<string, object> { { "name", "Alex" }, { "age", 20 } });
            throw new NotImplementedException();
        }

        public IList<Record> Select(IList<string> columns, string table, IDictionary<string, object> where)
        {
            throw new NotImplementedException();
        }
    }
}
