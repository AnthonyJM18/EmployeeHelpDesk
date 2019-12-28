/* Program Name : DepartmentViewModel
 * Author       : Anthony Merante
 * Date         : October 23, 2019
 * Purpose      : Creates a view model for departments
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using HelpdeskDAL;

namespace HelpdeskViewModels
{
    /* DepartmentViewModel Class
     * Provides methods to allow the user to perform CRUD operations without talking to the databases directly (acts as an intermediary)
     * Acts as a model for the department class
     */
    public class DepartmentViewModel
    {
        private DepartmentModel _model;


        public int Id { get; set; }
        public string Name { get; set; }
        public DepartmentViewModel()
        {
            _model = new DepartmentModel();
        }

        public List<DepartmentViewModel> GetAll()
        {
            List<DepartmentViewModel> allVms = new List<DepartmentViewModel>();
            try
            {
                List<Departments> allDepartment = _model.GetAll();

                foreach (Departments div in allDepartment)
                {
                    DepartmentViewModel divvm = new DepartmentViewModel();
                    divvm.Id = div.Id;
                    divvm.Name = div.DepartmentName;
                    allVms.Add(divvm);
                }
            }
            catch (Exception ex)
            {
                Name = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allVms;
        }
    }
}
