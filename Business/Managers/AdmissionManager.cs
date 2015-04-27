using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MigraDoc.DocumentObjectModel;
using PdfSharp;
using Common.Models;

namespace Business.Managers
{
    public class AdmissionManager : IAdmissionManager
    {
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
                BaccalaureatMaximumGrade = 9.80,
                FinalGrade= 9.28
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
                BaccalaureatMaximumGrade = 9.5,
                FinalGrade = 0.5* 9.20 + 0.25*9.50+0.25*9.50
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
                BaccalaureatMaximumGrade = 9.2,
                FinalGrade = 0.5* 9.30 + 0.25*9.10 + 0.25*9.20
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
                BaccalaureatMaximumGrade = 9.2,
                FinalGrade = 0.5* 9.30 + 0.25*9.10 + 0.25*9.20
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
                BaccalaureatMaximumGrade = 9.2,
                FinalGrade = 0.5* 9.30 + 0.25*9.10 + 0.25*9.20
            }
        };

        public void computeResult(Admission admission)
        {
            //TODO - change this.Students with students from param
            this.Students = this.Students.OrderBy(s => s.FinalGrade)
                               .ThenBy(s => s.AdmissionExamGrade)
                               .ThenBy(s => s.BaccalaureatAverageGrade)
                               .ThenBy(s => s.BaccalaureatMaximumGrade)
                               .Reverse()
                               .ToList();

            //foreach (Student s in this.Students)
            //{
            //    Console.WriteLine(s.FirstName + " " + s.FinalGrade + " " + s.AdmissionExamGrade + " " + s.BaccalaureatAverageGrade + " " + s.BaccalaureatMaximumGrade);
            //}
        }

        public void classifyCandidates(List<Student> students, int budget, int tax)
        {
            int b = 2, t = 2 ;
            //TODO - change test data with param data

            for (int i = 0; i < this.Students.Count; i++)
            {
                if (i < b)
                {
                    this.Students.ElementAt(i).Status = "BudgetFinanced";
                } else if (i>=b && i<b+t){
                    this.Students.ElementAt(i).Status = "FeePayer";
                }
                else
                {
                    this.Students.ElementAt(i).Status = "Rejected";
                }
            }

            DateTime year = new DateTime();
            string filePath = "D:\\_FII\\1.2\\_CSS\\faa" + "\\FAA" + year.Year + ".cvs";

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            string delimiter = ",";

            using (System.IO.TextWriter writer = File.CreateText(filePath))
            {
                for (int j = 0; j < this.Students.Count; j++)
                {
                    Student s = this.Students.ElementAt(j);
                    writer.WriteLine(string.Join(delimiter, s.FirstName, s.FinalGrade, s.AdmissionExamGrade, s.BaccalaureatAverageGrade, s.BaccalaureatMaximumGrade));
                }
            }

            Common.GeneratePDFResults pdfGenerator = new Common.GeneratePDFResults();
            Document doc = pdfGenerator.GenerateResults(this.Students);

            MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(doc, "MigraDoc.mdddl");
            MigraDoc.Rendering.PdfDocumentRenderer renderer = new MigraDoc.Rendering.PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            renderer.Document = doc;

            renderer.RenderDocument();

            string fileName = "FIIAdmission.pdf";
            renderer.PdfDocument.Save(fileName);

            
        }

        public List<Student> clasifyAll()
        {
            return this.Students; //TODO
        }


    }
}
