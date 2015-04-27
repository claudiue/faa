using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp;
using MigraDoc.DocumentObjectModel;
using Common.Models;

namespace Common
{
    public class GeneratePDFResults
    {
        private MigraDoc.DocumentObjectModel.Document document;
        private MigraDoc.DocumentObjectModel.Tables.Table table;

        //public GeneratePDFResults(List<Student> students)
        //{
        //    Document doc = Document(students);

        //}

        public Document GenerateResults(List<Student> students)
        {
            this.document = new MigraDoc.DocumentObjectModel.Document();
            this.document.Info.Title = "Faculty Admission Results";

            DefineStyles();
            CreatePage(students);
            return this.document;
        }

        protected void DefineStyles()
        {
           Style style = this.document.Styles["Normal"];

            style.Font.Name = "Verdana";
            style = this.document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = this.document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            style = this.document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 8;

            style = this.document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
        }

        protected void CreatePage(List<Student> students)
        {
            Section section = this.document.AddSection();

            this.table = section.AddTable();
            this.table.Style = "Table";
            this.table.Borders.Color = Colors.Black;
            this.table.Borders.Width = 0.25;
            this.table.Borders.Left.Width = 0.5;
            this.table.Borders.Right.Width = 0.5;
            this.table.Rows.LeftIndent = 0;

            MigraDoc.DocumentObjectModel.Tables.Column column = this.table.AddColumn("4cm"); //first name, father initial, last name
            column.Format.Alignment = ParagraphAlignment.Center;

            column = this.table.AddColumn("3cm");//specialization
            column.Format.Alignment = ParagraphAlignment.Left;

            column = this.table.AddColumn("2cm"); //final grade
            column.Format.Alignment = ParagraphAlignment.Left;

            column = this.table.AddColumn("2cm"); //addmision grade
            column.Format.Alignment = ParagraphAlignment.Left;

            column = this.table.AddColumn("2cm"); //baccalaureat average grade
            column.Format.Alignment = ParagraphAlignment.Left;

            column = this.table.AddColumn("2cm"); //baccalaureat maximum grade
            column.Format.Alignment = ParagraphAlignment.Left;

            column = this.table.AddColumn("3cm"); //status 
            column.Format.Alignment = ParagraphAlignment.Left;

            MigraDoc.DocumentObjectModel.Tables.Row row = this.table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Borders.Bottom.Visible = true;
            row.Borders.Bottom.Width = 1;

            row.Cells[0].AddParagraph("Student");
            row.Cells[1].AddParagraph("Specialization");
            row.Cells[2].AddParagraph("Final Grade");
            row.Cells[3].AddParagraph("Admission");
            row.Cells[4].AddParagraph("Average");
            row.Cells[5].AddParagraph("Maximum");
            row.Cells[6].AddParagraph("Status");

            for (int j = 0; j < 7; j++)
            {
                row.Cells[j].Format.Font.Bold = true;
                row.Cells[j].Format.Alignment = ParagraphAlignment.Justify;
                row.Cells[j].Borders.Bottom.Width = 0.25;
            }

            for (int i = 0; i < students.Count; i++)
            {
                row = this.table.AddRow();
                Student current = students.ElementAt(i);
                row.Cells[0].AddParagraph(current.FirstName + " " + current.FatherInitial + " " + current.LastName);
                row.Cells[1].AddParagraph(current.Specialization);
                row.Cells[2].AddParagraph(current.FinalGrade.ToString());
                row.Cells[3].AddParagraph(current.AdmissionExamGrade.ToString());
                row.Cells[4].AddParagraph(current.BaccalaureatAverageGrade.ToString());
                row.Cells[5].AddParagraph(current.BaccalaureatMaximumGrade.ToString());
                row.Cells[6].AddParagraph(current.Status);


                row.Borders.Bottom.Visible = true;
                row.Borders.Bottom.Width = 0.25;
            }

        }
    }
}
