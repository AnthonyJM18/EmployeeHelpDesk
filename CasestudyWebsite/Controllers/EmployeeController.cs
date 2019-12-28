/* Program Name : EmployeeController
 * Author       : Anthony Merante
 * Date         : October 23, 2019
 * Purpose      : Controller for Employee APIs
 */

using System;
using Microsoft.AspNetCore.Mvc;
using HelpdeskViewModels;
using Microsoft.AspNetCore.Http;
using System.Reflection;    // catch
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Info3070Exercises.controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // Creates a log that will store information about caught exceptions
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

       
        /* HttpPut Method
         * Update API which will update an employee
         */
        [HttpPut]
        public IActionResult Put(EmployeeViewModels viewModel)
        {
            try
            {
                int retVal = viewModel.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok(new { msg = "Employees " + viewModel.Lastname + " updated!" });
                    case -1:
                        return Ok(new { msg = "Employees " + viewModel.Lastname + " not updated!" });
                    case -2:
                        return Ok(new { msg = "Data is stale for " + viewModel.Lastname + " not updated!" });
                    default:
                        return Ok(new { msg = "Employees " + viewModel.Lastname + " not updated!" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /* HttpGet Method
         * Gets all of the employees
         */
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                EmployeeViewModels viewModel = new EmployeeViewModels();
                List<EmployeeViewModels> allEmployees= viewModel.GetAll();
                return Ok(allEmployees);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /* HttpGet Method
        * Parameter : Email
        * Return : Employee with that specified email if it exists
        */
        [HttpGet("{email}")]
        public IActionResult GetByEmail(string email)
        {
            try
            {
                EmployeeViewModels viewModel = new EmployeeViewModels();
                viewModel.Email = email;
                viewModel.GetByEmail();
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
           
        /* HttpPost Method
         * Adds an employee to the database
         */
        [HttpPost]
        public IActionResult Post(EmployeeViewModels viewModel)
        {
            try
            {
                viewModel.Add();
                return viewModel.Id > 1 ?
                    Ok(new { msg = "Employee " + viewModel.Lastname + " added!" })
                    : Ok(new { msg = "Employee " + viewModel.Lastname + " not added!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /* HttpDelete Method
         * Accepts : Employee ID as an int
         * Deletes an employee from the database
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                EmployeeViewModels viewModel = new EmployeeViewModels();
                viewModel.Id = id;
                return viewModel.Delete() == 1 ?
                    Ok(new { msg = "Employee " + id + " deleted!" }) :
                    Ok(new { msg = "Employee " + id + " not deleted!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}