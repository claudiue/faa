using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NMock2;
using Business.Managers;
using Common.Models;
using Common;
using DataLayer;

namespace Business.Test
{
    [TestFixture]
    class StudentManagerTest
    {
        private Mockery mocks;
        private IDataAccess mockDataAccess;
        private IStudentManager studentManager;
        private List<Record> testRecords;
        
        [SetUp]
        public void SetUp()
        {
            this.mocks = new Mockery();
            this.mockDataAccess = mocks.NewMock<IDataAccess>();
            this.studentManager = new StudentManager(this.mockDataAccess);

            #region test_data
            this.testRecords = new List<Record>() {
                new Record{
                    Fields = {
                        {"id", 1},
                        {"first_name", "Firstname1"},
                        {"last_name", "Lastname1"},
                        {"father_initial", "A"},
                        {"pin", "10000000001"},
                        {"city", "City1"},
                        {"address", "Address1"},
                        {"highschool", "Highschool1"},
                        {"specialization", "Spec1"},
                        {"admission_exam_grade", 8.10D},
                        {"baccalaureat_average_grade", 8.75D},
                        {"baccalaureat_maximum_grade", 9.00D}
                    }
                },
                new Record{
                    Fields = {
                        {"id", 2},
                        {"first_name", "Firstname2"},
                        {"last_name", "Lastname2"},
                        {"father_initial", "B"},
                        {"pin", "10000000002"},
                        {"city", "City1"},
                        {"address", "Address2"},
                        {"highschool", "Highschool2"},
                        {"specialization", "Spec2"},
                        {"admission_exam_grade", 8.80D},
                        {"baccalaureat_average_grade", 7.45D},
                        {"baccalaureat_maximum_grade", 7.70D}
                    }
                },
                new Record{
                    Fields = {
                        {"id", 3},
                        {"first_name", "Firstname3"},
                        {"last_name", "Lastname3"},
                        {"father_initial", "C"},
                        {"pin", "10000000003"},
                        {"city", "City3"},
                        {"address", "Address3"},
                        {"highschool", "Highschool3"},
                        {"specialization", "Spec3"},
                        {"admission_exam_grade", 7.60D},
                        {"baccalaureat_average_grade", 9.05D},
                        {"baccalaureat_maximum_grade", 8.50D}
                    }
                }
            };
            #endregion test_data
        }

        [Test]
        public void Should_Get_All_Students()
        {
            Expect.Once.On(this.mockDataAccess).
                Method("Select").
                With(NMock2.Is.EqualTo("FAA-2015"), IsList.Equal(new List<string> { "*" }), NMock2.Is.EqualTo("students1"), IsDictionary.Equal(new Dictionary<string, object> { })).
                Will(Return.Value(this.testRecords));

            List<Student> studentList = this.studentManager.GetAll();

            Assert.AreEqual(3, studentList.Count);
            Assert.IsInstanceOf<List<Student>>(studentList);

            this.mocks.VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        public void Should_Get_One_By_Id()
        {
            int id = 1;

            Expect.Once.On(this.mockDataAccess).
                Method("Select").
                With(NMock2.Is.EqualTo("FAA-2015"), IsList.Equal(new List<string> { "*" }), NMock2.Is.EqualTo("students1"), IsDictionary.Equal(new Dictionary<string, object> { { "id", id } })).
                Will(Return.Value(new List<Record>() { this.testRecords[0] }));

            Student student = this.studentManager.GetOneById(id);

            Assert.AreEqual(1, student.Id);
            Assert.AreEqual("Firstname1", student.FirstName);
            Assert.AreEqual("Lastname1", student.LastName);
            Assert.AreEqual("A", student.FatherInitial);
            Assert.AreEqual("10000000001", student.PIN);
            Assert.AreEqual("City1", student.City);
            Assert.AreEqual("Address1", student.Address);
            Assert.AreEqual("Highschool1", student.Highschool);
            Assert.AreEqual("Spec1", student.Specialization);
            Assert.AreEqual(8.10D, student.AdmissionExamGrade);
            Assert.AreEqual(8.75D, student.BaccalaureatAverageGrade);
            Assert.AreEqual(9.00D, student.BaccalaureatMaximumGrade);

            this.mocks.VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        [ExpectedException(typeof(NonUniqueResultException))]
        public void Should_Get_One_By_Id_Throw_Error_If_Multiple_Results()
        {
            int id = 1;

            Expect.Once.On(this.mockDataAccess).
                Method("Select").
                With(NMock2.Is.EqualTo("FAA-2015"), IsList.Equal(new List<string> { "*" }), NMock2.Is.EqualTo("students1"), IsDictionary.Equal(new Dictionary<string, object> { { "id", id } })).
                Will(Return.Value(this.testRecords));

            Student student = this.studentManager.GetOneById(id);

            this.mocks.VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        [Ignore("TODO: review how to implement")]
        public void Should_Add_Student()
        {
            Student student = new Student() {
                Id = 4,
                FirstName = "Firstname4",
                LastName = "Lastname4",
                FatherInitial = "D",
                PIN = "10000000004",
                City = "City4",
                Address = "Address4",
                Highschool = "Highschool4",
                Specialization = "Spec4",
                AdmissionExamGrade = 7.75D,
                BaccalaureatAverageGrade = 8.50D,
                BaccalaureatMaximumGrade = 8.25D
            };
            //List<Record> records = new List<Record>();
            //records.Add(studentManager.GetRecordFromStudent(student));

            // TODO: review how to implement
        }

        [Test]
        [Ignore("TODO: review how to implement")]
        public void Should_Add_Student_List()
        {
            // TODO: review how to implement
        }

        [Test]
        [Ignore("TODO: review how to implement")]
        public void Should_Edit_Student()
        {
            // TODO: review how to implement
        }

        [Test]
        [Ignore("TODO: review how to implement")]
        public void Should_Delete_Student()
        {
            // TODO: review how to implement
        }
    }
}
