/* Program Name : EmployeeViewModels
 * Author       : Anthony Merante
 * Date         : October 23, 2019
 * Purpose      : Creates a view model for employees
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using HelpdeskDAL;

namespace HelpdeskViewModels
{
    /* EmployeeViewModel Class
     * Provides methods to allow the user to perform CRUD operations without talking to the databases directly (acts as an intermediary)
     * Acts as a model for the employee class
     */
    public class EmployeeViewModels
    {
        private EmployeeModel _model;
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phoneno { get; set; }
        public string Timer { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int Id { get; set; }
        public bool? IsTech { get; set; }
        public string StaffPicture64 { get; set; }
        
        public EmployeeViewModels()
        {
            _model = new EmployeeModel();
        }

        public void GetByEmail()
        {
            try
            {
                Employees emp = _model.GetByEmail(Email);
                Title = emp.Title;
                Firstname = emp.FirstName;
                Lastname = emp.LastName;
                Phoneno = emp.PhoneNo;
                Email = emp.Email;
                Id = emp.Id;
                DepartmentId = emp.DepartmentId;
                IsTech = emp.IsTech ?? false;
                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (NullReferenceException nex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + nex.Message);
                throw nex;
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        public void GetById()
        {
            try
            {

                Employees emp = _model.GetById(Id);
                Title = emp.Title;
                Firstname = emp.FirstName;
                Lastname = emp.LastName;
                Phoneno = emp.PhoneNo;
                Email = emp.Email;
                Id = emp.Id;
                DepartmentId = emp.DepartmentId;
                IsTech = emp.IsTech ?? false;
                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (NullReferenceException nex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + nex.Message);
                throw nex;
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public List<EmployeeViewModels> GetAll()
        {
            List<EmployeeViewModels> allVms = new List<EmployeeViewModels>();
            try
            {
                List<Employees> allEmployees = _model.GetAll();
                foreach (Employees emp in allEmployees)
                {
                    EmployeeViewModels empvm = new EmployeeViewModels();
                    empvm.Title = emp.Title;
                    empvm.Firstname = emp.FirstName;
                    empvm.Lastname = emp.LastName;
                    empvm.Phoneno = emp.PhoneNo;
                    empvm.Email = emp.Email;
                    empvm.Id = emp.Id;
                    empvm.DepartmentId = emp.DepartmentId;
                    empvm.DepartmentName = emp.Department.DepartmentName;
                    empvm.Timer = Convert.ToBase64String(emp.Timer);
                    empvm.IsTech = emp.IsTech ?? false;
                    if (emp.StaffPicture != null)
                    {
                        empvm.StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                    }
                    allVms.Add(empvm);
                }
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allVms;
        }

        public void Add()
        {
            Id = -1;
            try
            {
                Employees emp = new Employees();
                emp.Title = Title;
                emp.FirstName = Firstname;
                emp.LastName = Lastname;
                emp.PhoneNo = Phoneno;
                emp.Email = Email;
                emp.DepartmentId = DepartmentId;
                emp.IsTech = IsTech;
                 if (StaffPicture64 != null)
                {
                    emp.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }
                Id = _model.Add(emp);
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public int Update()
        {
            UpdateStatus EmployeesUpdated = UpdateStatus.Failed;
            try
            {
                Employees emp = new Employees();
                emp.Title = Title;
                emp.FirstName = Firstname;
                emp.LastName = Lastname;
                emp.PhoneNo = Phoneno;
                emp.Email = Email;
                emp.Id = Id;
                emp.DepartmentId = DepartmentId;
                if (StaffPicture64 != null)
                {
                    emp.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }
                emp.Timer = Convert.FromBase64String(Timer);
                EmployeesUpdated = _model.Update(emp);
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return (int)EmployeesUpdated;
        }
        public int Delete()
        {
            int EmployeesDeleted = -1;
            try
            {
                EmployeesDeleted = _model.Delete(Id);
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return EmployeesDeleted;
        }
    }
}

