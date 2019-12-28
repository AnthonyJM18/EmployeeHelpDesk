using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;


namespace HelpdeskDAL
{
    public class CallModel
    {
        IRepository<Calls> repository;
        public CallModel()
        {
            repository = new SomeHelpdeskRepository<Calls>();
        }

        public Calls GetById(int id)
        {
            try
            {
                return repository.GetByExpression(call => call.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        
        public List<Calls> GetAll()
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

        public int add(Calls newCall)
        {  
            try
            {
                return repository.Add(newCall).Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public UpdateStatus Update (Calls updatedCall)
        {
            try
            {
                return repository.Update(updatedCall);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

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
