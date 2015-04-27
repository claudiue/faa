using Business;
using Business.Managers;
using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class StudentsController : Controller
    {
        private IStudentManager _studentManager;
        private IAdmissionManager _admissionManager;

        public StudentsController()
        {
            _studentManager = BusinessFactory.CreateStudentManager();
            _admissionManager = BusinessFactory.CreateAdmissionManager();
        }

        public ActionResult Index()
        {
            var students = _studentManager.GetAll();
            return View(students);
        }

        public ActionResult Results()
        {
            var students = _studentManager.GetAll();
            var addmissionModel = new Admission();
            var computed = _admissionManager.ComputeResult(addmissionModel, students);
            var _sorted = _admissionManager.ClassifyCandidates(computed, 3, 3);
            return View(_sorted);
        }

        public ActionResult GeneratePDF()
        {
            var students = _studentManager.GetAll();
            var addmissionModel = new Admission();
            var computed = _admissionManager.ComputeResult(addmissionModel, students);
            var _sorted = _admissionManager.ClassifyCandidates(computed, 3, 3);
            _admissionManager.ExportToPDF(_sorted);
            return View("Results", _sorted);
        }

        public ActionResult GenerateCSV()
        {
            var students = _studentManager.GetAll();
            var addmissionModel = new Admission();
            var computed = _admissionManager.ComputeResult(addmissionModel, students);
            var _sorted = _admissionManager.ClassifyCandidates(computed, 3, 3);
            _admissionManager.ExportToCSV(_sorted);
            return View("Results", _sorted);
        }
    }
}