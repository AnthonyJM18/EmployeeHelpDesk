using System;
using System.Collections.Generic;
using System.Reflection;
using HelpdeskDAL;

namespace HelpdeskViewModels
{
    public class CallViewModel
    {
        private CallModel _model;
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ProblemId { get; set; }
        public string EmployeeName { get; set; }
        public string ProblemDescription { get; set; }
        public string TechName { get; set; }
        public int TechId { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }
        public bool OpenStatus { get; set; }
        public string Notes { get; set; }
        public string Timer { get; set; }
        public CallViewModel()
        {
            _model = new CallModel();
        }

        public void GetById()
        {
            try
            {
                Calls call = _model.GetById(Id);
                EmployeeId = call.EmployeeId;
                ProblemId = call.ProblemId;
                EmployeeName = call.Employee.FirstName + " " + call.Employee.LastName;
                TechName = call.Tech.FirstName + " " + call.Tech.LastName;
                ProblemDescription = call.Problem.Description;
                TechId = call.TechId;
                DateOpened = call.DateOpened;
                DateClosed = call.DateClosed;
                OpenStatus = call.OpenStatus;
                Notes = call.Notes;
                Timer = Convert.ToBase64String(call.Timer);
            }
            catch (NullReferenceException nex)
            {
                Notes = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + nex.Message);
                throw nex;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public List<CallViewModel> GetAll()
        {
            List<CallViewModel> allVms = new List<CallViewModel>();
            try
            {
                List<Calls> allCalls = _model.GetAll();
                foreach (Calls call in allCalls)
                {
                    CallViewModel callvm = new CallViewModel();
                    callvm.Id = call.Id;
                    callvm.EmployeeId = call.EmployeeId;
                    callvm.ProblemId = call.ProblemId;
                    callvm.TechId = call.TechId;
                    callvm.EmployeeName = call.Employee.FirstName + " "+  call.Employee.LastName;
                    callvm.TechName = call.Tech.FirstName + " " + call.Tech.LastName;
                    callvm.ProblemDescription = call.Problem.Description;
                    callvm.DateOpened = call.DateOpened;
                    callvm.DateClosed = call.DateClosed;
                    callvm.OpenStatus = call.OpenStatus;
                    callvm.Notes = call.Notes;
                    callvm.Timer = Convert.ToBase64String(call.Timer);
                    allVms.Add(callvm);
                }
            }
            catch (Exception ex)
            {
                Notes = "not found";
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
                Calls call = new Calls();
                call.EmployeeId = EmployeeId;
                call.ProblemId = ProblemId;
                call.TechId = TechId;
                call.DateOpened = DateOpened;
                call.DateClosed = DateClosed;
                call.OpenStatus = OpenStatus;
                call.Notes = Notes;
                Id = _model.add(call);
            }
            catch (Exception ex)
            {
                Notes = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public int Update()
        {
            UpdateStatus CallUpdated = UpdateStatus.Failed;
            try
            {
                Calls call = new Calls();
                call.Id = Id;
                call.EmployeeId = EmployeeId;
                call.ProblemId = ProblemId;
                call.TechId = TechId;
                call.DateOpened = DateOpened;
                call.DateClosed = DateClosed;
                call.OpenStatus = OpenStatus;
                call.Notes = Notes;
                call.Timer = Convert.FromBase64String(Timer);
                CallUpdated = _model.Update(call);
            }
            catch (Exception ex)
            {
                Notes = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return (int)CallUpdated;
        }

        public int Delete()
        {
            int CallDeleted = -1; 
            try
            {
                CallDeleted = _model.Delete(Id);
            }
            catch (Exception ex)
            {
                Notes = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return CallDeleted;
        }
    }
}
