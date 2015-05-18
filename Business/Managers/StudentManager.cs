using Common;
using Common.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Business.Managers
{
    public class StudentManager : IStudentManager
    {
        private IDataAccess dataAccess;

        public StudentManager(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public List<Student> GetAll()
        {
            // TODO: delete this when not needed
            //return new List<Student>();

            List<Record> records = (List<Record>)this.dataAccess.Select("FAA-2015", new List<string> { "*" }, "students1", new Dictionary<string, object> { });

            List<Student> studentList = new List<Student>();
            foreach (Record record in records)
            {
                Student student = this.GetStudentFromRecord(record);
                studentList.Add(student);
            }

            return studentList;
        }

        public Student GetOneById(int id)
        {
            // TODO: delete this when not needed
            //return new Student();
            List<Record> students = (List<Record>)this.dataAccess.Select("FAA-2015", new List<string> { "*" }, "students1", new Dictionary<string, object> { { "id", id } });

            Trace.Assert(students.Count == 1);
            if (students.Count != 1)
            {
                throw new NonUniqueResultException("Multiple results returned...");
            }

            Record record = students.First();
            Student student = this.GetStudentFromRecord(record);

            return student;
        }

        public void Add(Student student)
        {
            List<Record> records = new List<Record>();
            records.Add(this.GetRecordFromStudent(student));

            this.dataAccess.Insert("FAA-2015", "students1", records);
        }

        public void AddList(List<Student> students)
        {
            List<Record> records = new List<Record>();
            
            for (int i = 0; i < students.Count; i++)
            {
                records.Add(this.GetRecordFromStudent(students[i]));
                Trace.Assert(students.Count - i > 0);
            }
            Trace.Assert(records.Count == students.Count);

            this.dataAccess.Insert("FAA-2015", "students1", records);
        }

        public void Edit(int id, Student student)
        {
            Record record = this.GetRecordFromStudent(student);
            this.dataAccess.Update("FAA-2015", "students1", record.Fields, new Dictionary<string, object> { { "id", id } });
        }

        public void Delete(int id)
        {
            this.dataAccess.Delete("FAA-2015", "students1", new Dictionary<string, object> { { "id", id } });
        }

        private Student GetStudentFromRecord(Record record)
        {
            // TODO: make sure if the key does not exist it does not throw exception
            Student student = new Student();
            student.Id = (int)record.Fields["id"];
            student.FirstName = (string)record.Fields["first_name"];
            student.LastName = (string)record.Fields["last_name"];
            student.FatherInitial = (string)record.Fields["father_initial"];
            student.PIN = (string)record.Fields["pin"];
            student.City = (string)record.Fields["city"];
            student.Address = (string)record.Fields["address"];
            student.Highschool = (string)record.Fields["highschool"];
            student.Specialization = (string)record.Fields["specialization"];
            student.AdmissionExamGrade = (double)record.Fields["admission_exam_grade"];
            student.BaccalaureatAverageGrade = (double)record.Fields["baccalaureat_average_grade"];
            student.BaccalaureatMaximumGrade = (double)record.Fields["baccalaureat_maximum_grade"];

            return student;
        }

        private Record GetRecordFromStudent(Student student)
        {
            // TODO: make sure if the key does not exist it does not throw exception
            Record record = new Record();
            record.Fields.Add("id", student.Id);
            record.Fields.Add("first_name", student.FirstName);
            record.Fields.Add("last_name", student.LastName);
            record.Fields.Add("father_initial", student.FatherInitial);
            record.Fields.Add("pin", student.PIN);
            record.Fields.Add("city", student.City);
            record.Fields.Add("address", student.Address);
            record.Fields.Add("highschool", student.Highschool);
            record.Fields.Add("specialization", student.Specialization);
            record.Fields.Add("admission_exam_grade", student.AdmissionExamGrade);
            record.Fields.Add("baccalaureat_average_grade", student.BaccalaureatAverageGrade);
            record.Fields.Add("baccalaureat_maximum_grade", student.BaccalaureatMaximumGrade);

            return record;
        }
    }

    public class NonUniqueResultException : ApplicationException
    {
        public string Description { get; set; }

        public NonUniqueResultException(string message)
        {
            this.Description = message;
        }
    }
}
