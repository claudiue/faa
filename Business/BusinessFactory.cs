using Business.Managers;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessFactory
    {
        public static IStudentManager CreateStudentManager()
        {
            return new StudentManager(new DataFactory().CreateDataAccess());
        }

        public static IAdmissionManager CreateAdmissionManager()
        {
            return new AdmissionManager(new StudentManager(new DataFactory().CreateDataAccess()));
        }
    }
}
