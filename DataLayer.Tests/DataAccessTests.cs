using Common.Models;
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

        [Test]
        public void Should_Create_Database()
        {
            var column1 = new Column("Id", typeof(int), 0);
            var column2 = new Column("Name", typeof(string), 1);

            var table = new Table("TableOne", column1, column2);
            var database = new Database("TestDB", table);

            var sut = new Factory().CreateDataAccess();
            sut.CreateDatabase(database);
        }

    }
}
