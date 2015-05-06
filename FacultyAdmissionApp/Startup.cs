using Microsoft.Owin;
using Owin;
using Business.Managers;
using Common.Models;
using System.Collections.Generic;

[assembly: OwinStartupAttribute(typeof(FacultyAdmissionApp.Startup))]
namespace FacultyAdmissionApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);

            //AdmissionManager am = new AdmissionManager();

            //am.computeResult(new Admission());
            //am.classifyCandidates(new List<Student>(), 2, 2);
        }
    }
}
