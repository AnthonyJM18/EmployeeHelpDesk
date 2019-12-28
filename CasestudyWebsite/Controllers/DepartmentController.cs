/* Program Name : DepartmentController
 * Author       : Anthony Merante
 * Date         : October 23, 2019
 * Purpose      : Controller for Department Apis
 */

using System;
using Microsoft.AspNetCore.Mvc;
using HelpdeskViewModels;
using Microsoft.AspNetCore.Http;
using System.Reflection;    // catch
using Microsoft.Extensions.Logging;
using System.Collections.Generic;


namespace CasestudyWebsite.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        // this creates a log that exceptions will be logged to
        private readonly ILogger _logger;
        public DepartmentController(ILogger<DepartmentController> logger)
        {
            _logger = logger;
        }

        /* HttpGet Method
         * Gets all Divisions
         * */
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                DepartmentViewModel viewModel = new DepartmentViewModel();
                List<DepartmentViewModel> allStudents = viewModel.GetAll();
                return Ok(allStudents);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}