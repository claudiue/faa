﻿using Common;
using Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

        public void Insert(string db, string table, IList<Record> records)
        {
            throw new NotImplementedException();
        }

        public void Update(string db, string table, IDictionary<string, object> set, IDictionary<string, object> where)
        {
            throw new NotImplementedException();
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
            var lines = _fileManager.ReadFile(db, string.Format("{0}.csv", table));
            var firstLine = lines.FirstOrDefault();

            var keys = firstLine.Split(',');
            var cols = new List<Column>();

            for(var i = 0; i < keys.Length; i++)
            {
                var name = keys[i].Split(':')[0];
                if (!columns.Contains(name))
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

            return records;
        }
    }
}
