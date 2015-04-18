using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Database : IDatabase
    {
        public string Name { get; private set; }
        public IList<ITable> Tables { get; private set; }

        public Database(string name, params ITable[] tables)
        {
            Name = name;
            Tables = tables;
        }
    }
}
