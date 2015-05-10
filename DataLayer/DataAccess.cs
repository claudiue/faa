using Common;
using Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
                _fileManager.WriteLine(database.Name, table.Name, table.ColumnsDefinition.ToString());
            }
        }

        public void DropDatabase(Database database)
        {
            _fileManager.DeleteFolder(database.Name);
        }

        public void Insert(string db, string table, IList<Record> records)
        {
            _fileManager.WriteList(db, table, records);
        }

        public IList<Record> Update(string db, string table, IDictionary<string, object> set, IDictionary<string, object> where)
        {
            //TODO save all records in a list
            var lines = _fileManager.ReadFile(db, table);
            var firstLine = lines.FirstOrDefault();
            var keys = firstLine.Split(',');
            var whereCols = new List<Column>();
            var setCols = new List<Column>();
            var records = new List<Record>();

            for (var i = 0; i < keys.Length; i++)
            {
                var name = keys[i].Split(':')[0];
                if (where.Keys.Contains(name))
                {
                    whereCols.Add(new Column
                    (
                        name: name,
                        type: Type.GetType(keys[i].Split(':')[1]),
                        index: i
                    ));
                }

                if (set.Keys.Contains(name))
                {
                    setCols.Add(new Column
                    (
                        name: name,
                        type: Type.GetType(keys[i].Split(':')[1]),
                        index: i
                    ));
                }
            }

            foreach (var line in lines.Skip(1))
            {
                var record = new Record();
                var values = line.Split(',');
                foreach (var column in whereCols)
                {
                    if (values[column.Index] == where.Values.First()) 
                    {
                        foreach (var col in setCols)
                        {
                            record.Fields[col.Name] = values[col.Index];
                        }
                    }
                }
            }
            return records;
        }

        public void Delete(string db, string table, IDictionary<string, object> where)
        {
            Select(
                db: "DB1",
                columns: new List<string> { "id", "name", "age" },
                table: "students",
                where: new Dictionary<string, object> { { "name", "Alex" }, { "age", 20 } });
            throw new NotImplementedException();
        }

        public IList<Record> Select(string db, IList<string> columns, string table, IDictionary<string, object> where)
        {
            var records = new List<Record>();
            var lines = _fileManager.ReadFile(db, table);
            var firstLine = lines.FirstOrDefault();

            var keys = firstLine.Split(',');
            var cols = new List<Column>();

            for(var i = 0; i < keys.Length; i++)
            {
                var name = keys[i].Split(':')[0];
                if (!columns.Contains("*") && !columns.Contains(name))
                    continue;

                cols.Add(new Column
                    (
                        name: name, 
                        type: Type.GetType(keys[i].Split(':')[1]),
                        index: i
                    ));
            }

            foreach (var line in lines.Skip(1)) 
            {
                var record = new Record();
                var values = line.Split(',');
                foreach(var column in cols)
                {
                    var converter = TypeDescriptor.GetConverter(column.Type);
                    record.Fields[column.Name] = converter.ConvertFromString(values[column.Index]);
                }
                records.Add(record);
            }

            if (where == null)
                return records;

            var filteredRecords = new List<Record>();
            foreach (var record in records)
            {
                var allCriteriaMatched = true;
                foreach(var whereKey in where.Keys)
                {
                    if (record.Fields.ContainsKey(whereKey) && !record.Fields[whereKey].Equals(where[whereKey]))
                        allCriteriaMatched = false;
                }
                if (allCriteriaMatched)
                    filteredRecords.Add(record);
            }

            return filteredRecords;
        }
    }
}
