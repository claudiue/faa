using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Table
    {
        public string Name { get; private set; }
        public IList<Column> Columns { get; private set; }
        public IList<Record> Records { get; private set; }

        public Table(string name, params Column[] columns)
        {
            Name = name;
            Columns = columns;
        }
    }
}
