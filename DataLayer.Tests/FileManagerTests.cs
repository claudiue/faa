using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tests
{
    [TestFixture]
    class FileManagerTests
    {
        private string _path;

        [SetUp]
        public void SetUp() 
        {
            _path = string.Format("{0}/TestFolder/", Config.Path);
        }

        [Test]
        [TestCase("TestFolder")]
        public void Should_Create_Directory(string name) 
        {
            IFileManager sut = new FileManager();
            sut.CreateFolder(name);
        }

        [Test]
        [TestCase("TestFolder", "FileOne")]
        [TestCase("TestFolder", "FileTwo")]
        [TestCase("TestFolder", "FileThree")]
        public void Should_Create_File(string path, string fileName)
        {
            IFileManager sut = new FileManager();
            sut.CreateFile(path, fileName);
        }

        [Test]
        [TestCase("TestFolder", "FileThree")]
        public void Should_Write_Line(string path, string fileName)
        {
            IFileManager sut = new FileManager();
            sut.WriteLine(path, fileName, "line");
        }

        [TearDown]
        public void TearDown() { }
    }
}
