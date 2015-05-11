using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using NMock2;

namespace Business.Test
{
    internal class ListMatcher : Matcher
    {
        private IList list;

        public ListMatcher(IList accounts)
        {
            this.list = accounts;
        }

        public override bool Matches(object o)
        {
            if (!(o is IList)) return false;
            IList otherList = (IList)o;

            if (list.Count != otherList.Count) return false;
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].Equals(otherList[i])) return false;
            }
            return true;
        }

        public override void DescribeTo(TextWriter writer)
        {
            writer.Write("List:");
            foreach (object o in list)
            {
                writer.Write(o.ToString() + " ");
            }
        }
    }

    internal class DictionaryMatcher : Matcher
    {
        private IDictionary dictionary;

        public DictionaryMatcher(IDictionary accounts)
        {
            this.dictionary = accounts;
        }

        public override bool Matches(object o)
        {
            if (!(o is IDictionary)) return false;
            IDictionary otherDictionary = (IDictionary)o;

            if (dictionary.Count != otherDictionary.Count)
                return false;

            return true;
        }

        public override void DescribeTo(TextWriter writer)
        {
            writer.Write("Dictionary:");
            foreach (KeyValuePair<object, object> entry in dictionary)
            {
                writer.Write(entry.Key.ToString() + " " + entry.Value.ToString() + "\n");
            }
        }
    }

    public class IsList
    {
        public static Matcher Equal(IList otherList)
        {
            return new ListMatcher(otherList);
        }
    }

    public class IsDictionary
    {
        public static Matcher Equal(IDictionary otherDictionary)
        {
            return new DictionaryMatcher(otherDictionary);
        }
    }
}
