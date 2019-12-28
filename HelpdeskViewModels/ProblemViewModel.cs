using System;
using System.Collections.Generic;
using System.Reflection;
using HelpdeskDAL;

namespace HelpdeskViewModels
{
    public class ProblemViewModel
    {
        private ProblemsModel _model;
        public int Id { get; set; }
        public string Description { get; set; }
        public ProblemViewModel()
        {
            _model = new ProblemsModel();
        }
        public void GetByDescription()
        {
            try
            {
                Problems prob = _model.GetByDescription(Description);
                Id = prob.Id;
            }
            catch (NullReferenceException nex)
            {
                Description = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + nex.Message);
                throw nex;
            }
            catch (Exception ex)
            {
                Description = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public List<ProblemViewModel> GetAll()
        {
            List<ProblemViewModel> allVms = new List<ProblemViewModel>();
            try
            {
                List<Problems> allProblems = _model.GetAll();
                foreach(Problems prob in allProblems)
                {
                    ProblemViewModel probvm = new ProblemViewModel();
                    probvm.Id = prob.Id;
                    probvm.Description = prob.Description;
                    allVms.Add(probvm);
                }
            }
            catch (Exception ex)
            {
                Description = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allVms;
        }
    }
}
