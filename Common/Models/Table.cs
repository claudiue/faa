using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Table : ITable
    {
        public string Name { get; private set; }
        public IList<IColumn> Columns { get; private set; }
        public IList<IRecord> Records { get; private set; }

        public Table(string name, params IColumn[] columns)
        {
            Name = name;
            Columns = columns;
        }
    }
}
