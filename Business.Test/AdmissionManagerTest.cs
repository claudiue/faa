using System;
using DataLayer;
using Business.Managers;
using Common.Models;
using Common;
using System.Linq; 
using System.Collections.Generic;

using NUnit.Framework;

namespace Business.Test
{

    [TestFixture]
    public class AdmissionManagerTest
    {
        #region test_data
        private List<Student> Students = new List<Student>() {
            new Student{
                Id = 1,
                FirstName = "Simona",
                LastName = "Serseniuc",
                FatherInitial = "D.",
                PIN = "1234567891234",
                City = "Iasi",
                Address = "Dacia",
                Highschool = "Liceul Teoretic Miron Costin Iasi",
                Specialization = "Informatica",
                AdmissionExamGrade = 9.70,
                BaccalaureatAverageGrade = 9.25,
                BaccalaureatMaximumGrade = 9.80
            },
            new Student {
                Id=2,
                FirstName = "Andreea",
                LastName = "Tiron",
                FatherInitial = "F.",
                PIN = "2345678912345",
                City = "Vaslui",
                Address = "Alexandru",
                Highschool = "Liceul Teoretic",
                Specialization = "Informatica",
                AdmissionExamGrade = 9.20,
                BaccalaureatAverageGrade = 9.50,
                BaccalaureatMaximumGrade = 9.5
            },
            new Student {
                Id=3,
                FirstName = "Emanuel",
                LastName = "Berea",
                FatherInitial = "V.",
                PIN = "2345678912345",
                City = "Piatra Neamt",
                Address = "1 Mai",
                Highschool = "Colegiul National de Informatica",
                Specialization = "Informatica",
                AdmissionExamGrade = 9.30,
                BaccalaureatAverageGrade = 9.10,
                BaccalaureatMaximumGrade = 9.2
            },
            new Student {
                Id=4,
                FirstName = "Emanuel 2",
                LastName = "Berea 2 ",
                FatherInitial = "V.",
                PIN = "2345678912345",
                City = "Piatra Neamt",
                Address = "1 Mai",
                Highschool = "Colegiul National de Informatica",
                Specialization = "Informatica",
                AdmissionExamGrade = 9.25,
                BaccalaureatAverageGrade = 9.10,
                BaccalaureatMaximumGrade = 9.2
            },
            new Student {
                Id=3,
                FirstName = "Emanuel 3 ",
                LastName = "Berea 3",
                FatherInitial = "V.",
                PIN = "2345678912345",
                City = "Piatra Neamt",
                Address = "1 Mai",
                Highschool = "Colegiul National de Informatica",
                Specialization = "Informatica",
                AdmissionExamGrade = 9.25,
                BaccalaureatAverageGrade = 9.30,
                BaccalaureatMaximumGrade = 9.2
            }
        };

        private List<Student> sortedStudents = new List<Student>() {
            new Student {
                Id=2,
                FirstName = "Andreea",
                LastName = "Tiron",
                FatherInitial = "F.",
                PIN = "2345678912345",
                City = "Vaslui",
                Address = "Alexandru",
                Highschool = "Liceul Teoretic",
                Specialization = "Informatica",
                AdmissionExamGrade = 9.20,
                BaccalaureatAverageGrade = 9.50,
                BaccalaureatMaximumGrade = 9.5
            },
            new Student{
                Id = 1,
                FirstName = "Simona",
                LastName = "Serseniuc",
                FatherInitial = "D.",
                PIN = "1234567891234",
                City = "Iasi",
                Address = "Dacia",
                Highschool = "Liceul Teoretic Miron Costin Iasi",
                Specialization = "Informatica",
                AdmissionExamGrade = 9.70,
                BaccalaureatAverageGrade = 9.25,
                BaccalaureatMaximumGrade = 9.80
            },
            
            new Student {
                Id=3,
                FirstName = "Emanuel",
                LastName = "Berea",
                FatherInitial = "V.",
                PIN = "2345678912345",
                City = "Piatra Neamt",
                Address = "1 Mai",
                Highschool = "Colegiul National de Informatica",
                Specialization = "Informatica",
                AdmissionExamGrade = 9.30,
                BaccalaureatAverageGrade = 9.10,
                BaccalaureatMaximumGrade = 9.2
            },
            new Student {
                Id=3,
                FirstName = "Emanuel 3 ",
                LastName = "Berea 3",
                FatherInitial = "V.",
                PIN = "2345678912345",
                City = "Piatra Neamt",
                Address = "1 Mai",
                Highschool = "Colegiul National de Informatica",
                Specialization = "Informatica",
                AdmissionExamGrade = 9.25,
                BaccalaureatAverageGrade = 9.30,
                BaccalaureatMaximumGrade = 9.2
            },
            new Student {
                Id=4,
                FirstName = "Emanuel 2",
                LastName = "Berea 2 ",
                FatherInitial = "V.",
                PIN = "2345678912345",
                City = "Piatra Neamt",
                Address = "1 Mai",
                Highschool = "Colegiul National de Informatica",
                Specialization = "Informatica",
                AdmissionExamGrade = 9.25,
                BaccalaureatAverageGrade = 9.10,
                BaccalaureatMaximumGrade = 9.2
            }
        };
        #endregion test_data
        
        [Test]
        public void DefaultCtrl_ShouldCreateNewInstance()
        {
            AdmissionManager admissionManager = new AdmissionManager();

            Assert.IsNotNull(admissionManager);

        }

        [Test]
        public void Should_Compute_Results()
        {            
            Admission admission = new Admission();
            AdmissionManager admissionManager = new AdmissionManager();

            this.sortedStudents = this.sortedStudents.OrderBy(s => s.FinalGrade)
                                   .ThenBy(s => s.AdmissionExamGrade)
                                   .ThenBy(s => s.BaccalaureatAverageGrade)
                                   .ThenBy(s => s.BaccalaureatMaximumGrade)
                                   .Reverse()
                                   .ToList();
            
            this.Students = (List<Student>)admissionManager.ComputeResult(admission, this.Students);

            Assert.IsNotNull(this.Students);
            Assert.AreEqual(this.Students.First().Id, this.sortedStudents.First().Id);
            Assert.AreEqual(this.Students.Last().Id, this.sortedStudents.Last().Id);
        }

        [Test]
        public void Should_Classiffy_Students()
        {
            Admission admission = new Admission();
            AdmissionManager admissionManager = new AdmissionManager();
            int budget = 2;
            int tax = 2;
            string Budget = "Budget";
            string Fee = "Fee";
            string Rejected = "Rejected";

            IList<Student> classiffiedStudents = admissionManager.ClassifyCandidates(this.Students, budget, tax);

            Assert.IsNotNull(this.Students);
            Assert.AreEqual(this.Students.ElementAt(0).Status, Budget);
            Assert.AreEqual(this.Students.ElementAt(1).Status, Budget);
            Assert.AreEqual(this.Students.ElementAt(2).Status, Fee);
            Assert.AreEqual(this.Students.ElementAt(3).Status, Fee);
            Assert.AreEqual(this.Students.ElementAt(4).Status, Rejected);
        }
    }
}
