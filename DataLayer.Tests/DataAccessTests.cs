using Common;
using Common.Models;
using NUnit.Framework;
using NUnit.Mocks;
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
        private DynamicMock _fileManagerMock;
        private IDataAccess _dataAccess;
        private IList<string> _lines;

        [SetUp]
        public void SetUp()
        {
            _lines = new List<string>
            {
                "id:System.Int32,first_name:System.String,last_name:System.String,father_initial:System.String,pin:System.String,city:System.String,address:System.String,highschool:System.String,specialization:System.String,admission_exam_grade:System.Double,baccalaureat_average_grade:System.Double,baccalaureat_maximum_grade:System.Double"
                ,"1,Andreea,Tiron,V,2910101010101,Vaslui,Visani,Mihail Kogalniceanu,mathematics,9.00,8.00,8.00"
                ,"2,Claudiu,Epure,C,1901414141414,Galati,Copou,Colegiul National,informatics,8.00,9.00,10.00"
                ,"3,Simona,Serseniuc,S,2921212121212,Iasi,Carol,Emil Racovita,geography,7.00,8.50,10.00"
                ,"4,Emanuel,Berea,T,1913434343434,Buzau,Stihii,Stefan Procopiu,history,10.00,9.00,9.00"
                ,"5,Sebastian,Tomescu,I,1885656565656,Bacau,Strugurilor,Vasile Alecsandri,mathematics,9.00,9.00,9.00"
                ,"6,Andreea,Haras,G,2859898989898,Suceava,Tacuta,Mihai Eminescu,informatics,9.00,10.00,10.00"
                ,"7,Laura,Grosu,M,2919797979797,Ploiesti,Parcului,Miron Costin,mathematics,10.00,10.00,10.00"
                ,"8,Andreea,Curca,O,2915858585858,Bucuresti,Fragilor,Mihail Sadoveanu,informatics,8.00,8.00,8.00"
                ,"9,Iuliana,Minea,P,2919292929292,Pitesti,Arcu,Grigore Moisil,mathematics,7.50,7.50,8.00"
            };
        }

        [Test]
        public void Should_Create_Database()
        {
            var column1 = new Column("Id", typeof(int), 0);
            var column2 = new Column("Name", typeof(string), 1);

            var table = new Table("TableOne", column1, column2);
            var database = new Database("TestDB", table);

            var sut = new DataFactory().CreateDataAccess();
            sut.CreateDatabase(database);
        }

        [Test]
        [TestCase(1, 2, "Claudiu")]
        [TestCase(1, 5, "Sebastian")]
        public void Select_Should_Filter_Data(int expectedCount, int id, string expectedFirstName)
        {
            _fileManagerMock = new DynamicMock(typeof(IFileManager));
            _fileManagerMock.ExpectAndReturn("ReadFile", _lines, "FAA-2015", "students");

            _dataAccess = new DataFactory().CreateDataAccess((IFileManager)_fileManagerMock.MockInstance);
            var results = _dataAccess.Select(db: "FAA-2015", 
                columns: new List<string> { "id", "first_name" }, 
                table: "students", 
                where: new Dictionary<string, object> { { "id", id } });

            Assert.That(results.Count, Is.EqualTo(expectedCount));
            Assert.That(results[0].Fields, Is.Not.Null);
            Assert.That(results[0].Fields["id"], Is.EqualTo(id));
            Assert.That(results[0].Fields["first_name"], Is.EqualTo(expectedFirstName));
        }

        [Test]
        [TestCase(0, -1)]
        public void Select_Should_Return_Empty_List_When_Where_Not_Match(int expectedCount, int id)
        {
            _fileManagerMock = new DynamicMock(typeof(IFileManager));
            _fileManagerMock.ExpectAndReturn("ReadFile", _lines, "FAA-2015", "students");

            _dataAccess = new DataFactory().CreateDataAccess((IFileManager)_fileManagerMock.MockInstance);
            var results = _dataAccess.Select(db: "FAA-2015",
                columns: new List<string> { "id", "first_name" },
                table: "students",
                where: new Dictionary<string, object> { { "id", id } });

            Assert.That(results.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        [TestCase(0, "TestColumn")]
        public void Select_Should_Return_Empty_List_When_Columns_Not_Match(int expectedCount, string inexistentColumn)
        {
            _fileManagerMock = new DynamicMock(typeof(IFileManager));
            _fileManagerMock.ExpectAndReturn("ReadFile", _lines, "FAA-2015", "students");

            _dataAccess = new DataFactory().CreateDataAccess((IFileManager)_fileManagerMock.MockInstance);
            var results = _dataAccess.Select(db: "FAA-2015",
                columns: new List<string> { inexistentColumn },
                table: "students",
                where: null);

            Assert.That(results.Count, Is.EqualTo(expectedCount));
        }

    }
}
