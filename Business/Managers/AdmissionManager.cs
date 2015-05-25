using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Diagnostics;
using System.Diagnostics.Contracts;
using MigraDoc.DocumentObjectModel;
using PdfSharp;
using Common.Models;
using Common;
using DataLayer;
using System.Diagnostics;

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

        public AdmissionManager()
        {
            
        }

        internal AdmissionManager (IStudentManager studentManager)
        {
            _studentManager = studentManager;
        }

        public IList<Student> ComputeResult(Admission admission, IList<Student> students)
        {
            //Contract.Requires<ArgumentException>(students.Count > 0, "The list should not be empty");
            //Contract.Ensures(students.Count > 0);

            Trace.Assert(students.Count > 0);
            students = students.OrderBy(s => s.FinalGrade)
                               .ThenBy(s => s.AdmissionExamGrade)
                               .ThenBy(s => s.BaccalaureatAverageGrade)
                               .ThenBy(s => s.BaccalaureatMaximumGrade)
                               .Reverse()
                               .ToList();

            Trace.Assert(students.Count > 0);
            return students;
        }

        public IList<Student> ClassifyCandidates(IList<Student> students, int budget, int tax)
        {
            //Contract.Requires<ArgumentNullException>(students != null);
            //Contract.Requires<ArgumentException>(students.Count > 0, "The list should not be empty");
            //Contract.Requires<ArgumentException>(budget > 0, "There should be budget students");
            //Contract.Requires<ArgumentException>(tax > 0, "There should be fee students");
            //Contract.Ensures(students.Count > 0, "Cannot return an empty list after classify");

            
            int b = budget, t = tax ;       

            for (int i = 0; i < students.Count; i++)
            {
                if (i < b)
                {
                    students.ElementAt(i).Status = "Budget";
                } else if ( i<b+t)
                {
                    students.ElementAt(i).Status = "Fee";
                }
                else if(i>=b+t)
                {
                    students.ElementAt(i).Status = "Rejected";
                }
            }

            return students;
        }


        public void ExportToCSV(IList<Student> students)
        {
            //Contract.Requires<ArgumentException>(students.Count > 0, "The list should not be empty");  

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

            Process.Start(filePath);
        }

        public void ExportToPDF(IList<Student> students)
        {
            //Contract.Requires<ArgumentException>(students.Count > 0, "The list should not be empty");  

            Common.GeneratePDFResults pdfGenerator = new Common.GeneratePDFResults();
            Document doc = pdfGenerator.GenerateResults(students);

            MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(doc, "MigraDoc.mdddl");
            MigraDoc.Rendering.PdfDocumentRenderer renderer = new MigraDoc.Rendering.PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            renderer.Document = doc;

            renderer.RenderDocument();

            string fileName = "FIIAdmission.pdf";
            renderer.PdfDocument.Save(string.Format(fileName));
            Process.Start(fileName);
        }

        public void GenerateNewAddmission()
        {
            var column1 = new Column("id", typeof(int), 0);
            var column2 = new Column("first_name", typeof(string),1);
            var column3 = new Column("last_name", typeof(string),2);
            var column4 = new Column("father_initial", typeof(string),3);            
            var column5 = new Column("pin", typeof(string),4);            
            var column6 = new Column("city", typeof(string),5);            
            var column7 = new Column("address", typeof(string),6); 
            var column8 = new Column("highschool", typeof(string),7);
            var column9 = new Column("specialization", typeof(string),8);
            var column10 = new Column("admission_exam_grade", typeof(double),9);
            var column11 = new Column("baccalaureat_average_grade", typeof(double),10);
            var column12 = new Column("baccalaureat_maximum_grade", typeof(double),11);

            Table student = new Table("Student", column1, column2, column3, column4, column5, column6, column7, column8, column9, column10, column11, column12);

            Database db = new Database("students2015", student);

        }

        
    }
}
