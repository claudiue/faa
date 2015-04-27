using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Column
    {
        public string Name { get; private set; }
        public Type Type { get; private set; }
        public int Index { get; set; }

        public Column(string name, Type type, int index)
        {
            Name = name;
            Type = type;
            Index = index;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Name, Type.ToString());
        }
    }
}
