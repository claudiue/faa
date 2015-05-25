using Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Contracts;

namespace Business.Managers
{
    [ContractClass(typeof(IAdmissionContract))]
    public interface IAdmissionManager
    {
        IList<Student> ComputeResult(Admission admission, IList<Student> students);
        IList<Student> ClassifyCandidates(IList<Student> students, int budget, int tax);
       
        void ExportToPDF(IList<Student> students);
        void ExportToCSV(IList<Student> students);
       // List<Student> clasifyByStatus(StudentStatus status);
       // void publish(AdmissionPublishFormat type);
    }
}
