using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public interface IStudentManager
    {
        List<Student> GetAll();
        Student GetOneById(int id);
        void Add(Student student);
        void AddList(List<Student> students);
        void Edit(int id, Student student);
        void Delete(int id);
    }
}
