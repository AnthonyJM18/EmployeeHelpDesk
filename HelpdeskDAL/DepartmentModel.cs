/* Program Name : DepartmentModel
 * Author       : Anthony Merante
 * Date         : October 23, 2019
 * Purpose      : Used to produce a department model
 */

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace HelpdeskDAL
{
    /* DepartmentModel Class*/
    public class DepartmentModel
    {
        // Make a Department type IRepository
        IRepository<Departments> repository;
        public DepartmentModel()
        {
            repository = new SomeHelpdeskRepository<Departments>();
        }

        // Method to get all departments and store them as a department list
        public List<Departments> GetAll()
        {
            try
            {
                return repository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
    }
}
