using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;


namespace HelpdeskDAL
{
    public class ProblemsModel
    {
        IRepository<Problems> repository;
        public ProblemsModel()
        {
            repository = new SomeHelpdeskRepository<Problems>();
        }
        public Problems GetByDescription(string desc)
        {
            try
            {
                return repository.GetByExpression(prob => prob.Description == desc).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }


        public List<Problems> GetAll()
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
