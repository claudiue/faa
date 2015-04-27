﻿using Common.Models;
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
        void Insert(string db, string table, IList<Record> records);
        void Update(string db, string table, IDictionary<string, object> set, IDictionary<string, object> where);
        void Delete(string db, string table, IDictionary<string, object> where);
        IList<Record> Select(string db, IList<string> columns, string table, IDictionary<string, object> where);
    }
}
