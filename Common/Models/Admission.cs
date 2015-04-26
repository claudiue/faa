using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    enum AdmissionPublishFormat { CSV, PDF };
    enum AdmissionStatus { Open, Processing, Closed };

    public class Admission : IAdmission
    {
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }
        public int BugetFinancedNo { get; set; }
        public int FeePayerNo { get; set; }
        public double BudgetFeeThreshold { get; set; }
        public double FeeRejectedThreshold { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ClosedAt { get; set; }
        public AdmissionStatus Status { get; set; }
    }
}
