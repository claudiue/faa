using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Database
    {
        public string Name { get; private set; }
        public IList<Table> Tables { get; private set; }

        public Database(string name, params Table[] tables)
        {
            Name = name;
            Tables = tables;
        }
    }
}
