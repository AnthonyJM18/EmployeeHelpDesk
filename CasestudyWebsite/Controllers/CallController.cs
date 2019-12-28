
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
    public class CallController : ControllerBase
    {
        // this creates a log that exceptions will be logged to
        private readonly ILogger _logger;
        public CallController(ILogger<CallController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                CallViewModel viewModel = new CallViewModel();
                List<CallViewModel> allCalls = viewModel.GetAll();
                return Ok(allCalls);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Post(CallViewModel viewModel)
        {
            try
            {
                viewModel.Add();
                return viewModel.Id > 1 ?
                    Ok(new { msg = "Call " + viewModel.Id + " added!" })
                    : Ok(new { msg = "Call " + viewModel.Id + " not added!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult Put(CallViewModel viewModel)
        {
            try
            {
                int retVal = viewModel.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok(new { msg = "Call " + viewModel.Id + " updated!" });
                    case -1:
                        return Ok(new { msg = "Call " + viewModel.Id + " not updated!" });
                    case -2:
                        return Ok(new { msg = "Data is stale for " + viewModel.Id + " not updated!" });
                    default:
                        return Ok(new { msg = "Call " + viewModel.Id + " not updated!" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                CallViewModel viewModel = new CallViewModel();
                viewModel.Id = id;
                return viewModel.Delete() == 1 ?
                    Ok(new { msg = "Call " + id + " deleted!" }) :
                    Ok(new { msg = "Call " + id + " not deleted!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}