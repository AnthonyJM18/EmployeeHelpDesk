using ExercisesWebsite.Reports;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ExercisesWebsite.Controllers
{

    public class ReportController : Controller
    {
        private IHostingEnvironment _env;
        public ReportController(IHostingEnvironment env)
        {
            _env = env;
        }

        [Route("api/employeereport")]
        public IActionResult GetEmployeeReport()
        {
            EmployeeReport employeeReport = new EmployeeReport();
            employeeReport.generateReport(_env.WebRootPath);
            return Ok(new { msg = "Report Generated" });

        }

        [Route("api/callreport")]
        public IActionResult GetCallReport()
        {
            CallReports callReport = new CallReports();
            callReport.generateReport(_env.WebRootPath);
            return Ok(new { msg = "Report Generated" });

        }
    }

   
}