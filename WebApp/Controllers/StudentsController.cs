using Business;
using Business.Managers;
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
    }
}