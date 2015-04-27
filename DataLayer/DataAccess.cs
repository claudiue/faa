using Common;
using Common.Models;
using System;
using System.Collections;
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
            for (var i = 0; i < database.Tables.Count; i++) 
            {
                var table = database.Tables[i];
                var columns = database.Tables[i].Columns;

                _fileManager.CreateFile(database.Name, table.Name);
                _fileManager.WriteLine(database.Name, table.Name, table.ColumnsDefinition.ToString());

                _fileManager.WriteList(database.Name, table.Name, table.Records);

                //IList<Record> records = new List<Record>();
                //foreach (Record r in database.Tables[i].Records)
                //{
                //    var name = database.Tables[i].Records;

                //    var type = database.Tables[i].Columns[i].Type;
                //    records.Add(new Record());
                //    IDictionary<string, object> fields = new Dictionary<string, object>(database.Tables[i].Records[i].Fields);
                //    fields.Keys;
                //}
                
                
            }
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
