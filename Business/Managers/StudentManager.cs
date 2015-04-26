using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.Models;

namespace Business.Managers
{
    public class StudentManager : IStudentManager
    {
        public List<Student> GetAll()
        {
            return new List<Student>();
        }

        public Student GetOneById(int id)
        {
            return new Student();
        }


        public void Add(Student student) { }
        public void AddList(List<Student> students) { }
        public void Edit(int id, Student student) { }
        public void Delete(int id) { }
    }
}
