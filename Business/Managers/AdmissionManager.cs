using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MigraDoc.DocumentObjectModel;
using PdfSharp;
using Common.Models;
using Common;

namespace Business.Managers
{
    public class AdmissionManager : IAdmissionManager
    {
        private IStudentManager _studentManager;

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
        #endregion test_data

        internal AdmissionManager (IStudentManager studentManager)
        {
            _studentManager = studentManager;
        }

        public IList<Student> ComputeResult(Admission admission, IList<Student> students)
        {
            //TODO - change this.Students with students from param
            students = students.OrderBy(s => s.FinalGrade)
                               .ThenBy(s => s.AdmissionExamGrade)
                               .ThenBy(s => s.BaccalaureatAverageGrade)
                               .ThenBy(s => s.BaccalaureatMaximumGrade)
                               .Reverse()
                               .ToList();
            return students;
            //foreach (Student s in this.Students)
            //{
            //    Console.WriteLine(s.FirstName + " " + s.FinalGrade + " " + s.AdmissionExamGrade + " " + s.BaccalaureatAverageGrade + " " + s.BaccalaureatMaximumGrade);
            //}
        }

        public IList<Student> ClassifyCandidates(IList<Student> students, int budget, int tax)
        {
            int b = budget, t = tax ;
            //TODO - change test data with param data

            for (int i = 0; i < students.Count; i++)
            {
                if (i < b)
                {
                    students.ElementAt(i).Status = "Budget";
                } else if (i>=b && i<b+t){
                    students.ElementAt(i).Status = "Fee";
                }
                else
                {
                    students.ElementAt(i).Status = "Rejected";
                }
            }

            return students;
        }

        public IList<Student> ClasifyAll()
        {
            return this.Students; //TODO
        }

        public void ExportToCSV(IList<Student> students)
        {
            DateTime year = new DateTime();
            string filePath = Config.Path + "\\FAA" + year.Year + ".csv";

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            string delimiter = ",";

            using (System.IO.TextWriter writer = File.CreateText(filePath))
            {
                for (int j = 0; j < students.Count; j++)
                {
                    Student s = students.ElementAt(j);
                    writer.WriteLine(string.Join(delimiter, 
                        s.Id, 
                        s.FirstName, 
                        s.LastName, 
                        s.FatherInitial,
                        s.PIN,
                        s.City,
                        s.Address,
                        s.Highschool,
                        s.Specialization,
                        s.AdmissionExamGrade, 
                        s.BaccalaureatAverageGrade, 
                        s.BaccalaureatMaximumGrade,
                        s.FinalGrade));
                }
            }
        }

        public void ExportToPDF(IList<Student> students)
        {
            Common.GeneratePDFResults pdfGenerator = new Common.GeneratePDFResults();
            Document doc = pdfGenerator.GenerateResults(students);

            MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(doc, "MigraDoc.mdddl");
            MigraDoc.Rendering.PdfDocumentRenderer renderer = new MigraDoc.Rendering.PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            renderer.Document = doc;

            renderer.RenderDocument();

            string fileName = "FIIAdmission.pdf";
            renderer.PdfDocument.Save(string.Format(fileName));
        }
    }
}
