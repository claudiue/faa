using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Managers;
using System.Diagnostics.Contracts;
using Common.Models;



namespace Business.Contracts
{
    [ContractClassFor(typeof(IAdmissionManager))]
    public abstract class IAdmissionContract : IAdmissionManager
    {
        IList<Common.Models.Student> IAdmissionManager.ClassifyCandidates(IList<Common.Models.Student> students, int budget, int tax)
        {
            Contract.Requires<ArgumentNullException>(students != null);
            Contract.Requires<ArgumentException>(students.Count > 0, "The list should not be empty");
            Contract.Requires<ArgumentException>(budget > 0, "There should be budget students");
            Contract.Requires<ArgumentException>(tax > 0, "There should be fee students");
            Contract.Ensures(students.Count > 0, "Cannot return an empty list after classify");

            return default(IList<Common.Models.Student>);
        }

        IList<Common.Models.Student> IAdmissionManager.ComputeResult(Common.Models.Admission admission, IList<Common.Models.Student> students)
        {
            Contract.Requires<ArgumentException>(students.Count > 0, "The list should not be empty");
            Contract.Ensures(students.Count > 0);

            return default(IList<Common.Models.Student>);
        }

        void IAdmissionManager.ExportToPDF(IList<Common.Models.Student> students)
        {
            Contract.Requires<ArgumentException>(students.Count > 0, "The list should not be empty");
        }

        void IAdmissionManager.ExportToCSV(IList<Common.Models.Student> students)
        {
            Contract.Requires<ArgumentException>(students.Count > 0, "The list should not be empty");
        }
    }

        
}
