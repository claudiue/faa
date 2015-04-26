using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public interface IAdmissionManager
    {
        void computeResult(Admission admission);
        List<Student> clasifyAll();
       // List<Student> clasifyByStatus(StudentStatus status);
       // void publish(AdmissionPublishFormat type);
    }
}
