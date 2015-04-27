using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tests
{
    [TestFixture]
    public class DataAccessTests
    {
        [Test]
        public void Should_Select_Records()
        {
            var sut = new Factory().CreateDataAccess();
            var results = sut.Select(db: "DB1", 
                columns: new List<string> { "id", "name" }, 
                table: "students", 
                where: new Dictionary<string, object> { { "id", 2 } });
        }
    }
}
