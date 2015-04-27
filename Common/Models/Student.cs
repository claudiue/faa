using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    enum StudentStatus { BudgetFinanced, FeePayer, Rejected };

    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherInitial { get; set; }
        public string PIN { get; set; } // CNP
        public string City { get; set; }
        public string Address { get; set; }
        public string Highschool { get; set; }
        public string Specialization { get; set; }
        public double AdmissionExamGrade { get; set; }
        public double BaccalaureatAverageGrade { get; set; }
        public double BaccalaureatMaximumGrade { get; set; }
        public double FinalGrade 
        { 
            get 
            { 
                return AdmissionExamGrade * 0.5 
                    + BaccalaureatAverageGrade * 0.25
                    + BaccalaureatMaximumGrade * 0.25; 
            } 
        }
        public string Status { get; set; }
        //public StudentStatus Status { get; set; }
    }
}
