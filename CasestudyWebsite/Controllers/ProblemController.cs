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
    public class ProblemController : ControllerBase
    {
        // this creates a log that exceptions will be logged to
        private readonly ILogger _logger;
        public ProblemController(ILogger<ProblemController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                ProblemViewModel viewModel = new ProblemViewModel();
                List<ProblemViewModel> allProblems = viewModel.GetAll();
                return Ok(allProblems);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}