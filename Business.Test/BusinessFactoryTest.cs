using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Business.Managers;

namespace Business.Test
{
    [TestFixture]
    class BusinessFactoryTest
    {
        [Test]
        public void Should_Be_Instance_Of_StudentManager()
        {
            Assert.IsInstanceOf<StudentManager>(BusinessFactory.CreateStudentManager());
        }

        [Test]
        public void Should_Be_Instance_Of_AdmissionManager()
        {
            Assert.IsInstanceOf<AdmissionManager>(BusinessFactory.CreateAdmissionManager());
        }
    }
}
