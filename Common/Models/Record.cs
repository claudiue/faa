using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Record
    {
        public IDictionary<string, object> Fields { get; set; }
        public string Values 
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<string, object> r in Fields) 
                {
                    sb.AppendFormat("{0},", r.Value);
                }
                return sb.ToString().TrimEnd(new char[] { ',' });
            }
        }

        public Record()
        {
            Fields = new Dictionary<string, object>();
        }
    }
}
