/* Program Name : EmployeeModel
 * Author       : Anthony Merante
 * Date         : October 23, 2019
 * Purpose      : Creates a model for the employee object
 */


using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace HelpdeskDAL
{
    /* EmployeeModel Class */
    public class EmployeeModel
    {
        // Create an employee type IRepository
        IRepository<Employees> repository;
        public EmployeeModel()
        {
            repository = new SomeHelpdeskRepository<Employees>();
        }

        // Method to get an employee by email
        public Employees GetByEmail(string email)
        {
            try
            {
                return repository.GetByExpression(ent => ent.Email == email).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

        }

        public Employees GetByLastName(string lastname)
        {
            try
            {
                return repository.GetByExpression(ent => ent.LastName == lastname).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        // Method to get an employee by ID
        public Employees GetById(int id)
        {
            try
            {
                return repository.GetByExpression(ent => ent.Id == id ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

        }

        // Method to get all employees
        public List<Employees> GetAll()
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
        
        // Method to add a new employee
        public int Add(Employees newEmployee)
        {
            try
            {
                return repository.Add(newEmployee).Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

        }

        // Method to update an employee
        public UpdateStatus Update(Employees updatedEmployee)
        {
            try
            {
                return repository.Update(updatedEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        // Method to delete an employee
        public int Delete(int id)
        { 
            try
            {
                return repository.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
    }
}

