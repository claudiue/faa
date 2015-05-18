using Common;
using Common.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tests
{
    [TestFixture]
    class FileManagerTests
    {
        private string _path;
        private string _longName;
        private IFileManager _sut;

        [SetUp]
        public void SetUp() 
        {
            _sut = new DataFactory().CreateFileManager();
            _path = string.Format("{0}/TestFolder/", Config.Path);

            _longName = string.Empty;
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < 270; i++) 
                sb.Append("a");
            _longName = sb.ToString();
        }

        [Test]
        [TestCase("")]
        public void Should_Create_Folder(string name)
        {
            _sut.CreateFolder(name);
            var path = String.Format("{0}/{1}", Config.Path, name);
            var directoryCreated = Directory.Exists(path);

            Assert.That(directoryCreated, Is.True);
        }

        [Test]
        //IOException
        [TestCase("test.txt")]
        //ArgumentException
        [TestCase(":Test")]
        //NotSupportedException
        [TestCase("Te:st")]
        //ArgumentNullException
        [TestCase("")]
        public void Create_Folder_Should_Not_Throw_Exceptions(string name) 
        {
            _sut.CreateFolder(name);
            Assert.DoesNotThrow(delegate { throw new Exception(); });
        }

        [Test]
        //PathTooLongException
        public void Create_Folder_Should_Not_Throw_PathTooLongException()
        {
            _sut.CreateFolder(_longName);
            Assert.DoesNotThrow(delegate { throw new PathTooLongException(); });
        }

        [Test]
        [TestCase("TestFolder", "FileOne")]
        [TestCase("TestFolder", "FileTwo")]
        [TestCase("TestFolder", "FileThree")]
        public void Should_Create_File(string path, string fileName)
        {
            _sut.CreateFile(path, fileName);

            fileName = String.Format("{0}/{1}/{2}.csv", Config.Path, path, fileName);
            var fileCreated = File.Exists(fileName);

            Assert.That(fileCreated, Is.True);
        }

        [Test]
        [TestCase("TestFolder", "FileThree", "Id, Name")]
        public void Should_Write_Line(string path, string fileName, string line)
        {
            _sut.WriteLine(path, fileName, line);
        }

        [Test]
        [TestCase("TestFolder", "", "test")]
        [TestCase("TestFolder", "FileThree", null)]
        public void Write_Line_Should_Throw_Exception(string path, string fileName, string line)
        {
            _sut.WriteLine(path, fileName, line);
        }

        [Test]
        [TestCase("TestFolder", "FileThree", "<script>alert(1);</script>")]
        public void Write_Line_Should_Sanitize_Input(string path, string fileName, string line)
        {
            _sut.WriteLine(path, fileName, line);
        }

        [Test]
        [TestCase("TestFolder", "")]
        public void Should_Write_List(string path, string fileName) 
        {
            IList<Record> records = new List<Record>();

            var firstRec = new Record();
            firstRec.Fields["id"] = 1;
            firstRec.Fields["name"] = "Andrei";

            var secondRec = new Record();
            secondRec.Fields["id"] = 2;
            secondRec.Fields["name"] = "Ion";

            records.Add(firstRec);
            records.Add(secondRec);

            _sut.WriteList(path, fileName, records);
        }

        [Test]
        [TestCase("TestDB")]
        public void Should_Delete_Folder(string path)
        {
            _sut.DeleteFolder(path);

            path = String.Format("{0}/{1}", Config.Path, path);
            var directoryExists = Directory.Exists(path);

            Assert.That(directoryExists, Is.False);
        }

        [TearDown]
        public void TearDown() { }
    }
}
