/* Program Name : HelpdeskRepository
 * Author       : Anthony Merante
 * Date         : October 23, 2019
 * Purpose      : Repository for model functions to peform CRUD operations
 */


using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HelpdeskDAL
{
    /* Class Name : SomeHelpdeskRepository
     * Interface  : IRepository<T>
     * Accepts    : Generic type ( T being related to HelpdeskEntity )
     */
    public class SomeHelpdeskRepository<T> : IRepository<T> where T : HelpdeskEntity
    {
        private HelpdeskContext _db = null;
        public SomeHelpdeskRepository(HelpdeskContext context = null)
        {
            _db = context != null ? context : new HelpdeskContext();
        }
        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }
        public List<T> GetByExpression(Expression<Func<T, bool>> match)
        {
            return _db.Set<T>().Where(match).ToList();
        }
        public T Add(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
            return entity;
        }
        public UpdateStatus Update(T updatedEntity)
        {

            UpdateStatus operationStatus = UpdateStatus.Failed;
            try
            {
                T currEntity = _db.Set<T>().FirstOrDefault(ent => ent.Id == updatedEntity.Id);
                _db.Entry(currEntity).OriginalValues["Timer"] = updatedEntity.Timer;
                _db.Entry(currEntity).CurrentValues.SetValues(updatedEntity);
                if (_db.SaveChanges() == 1)
                {
                    operationStatus = UpdateStatus.Ok;
                }
            }
            catch (DbUpdateConcurrencyException dbx)
            {
                // Data is stale andn does not update
                operationStatus = UpdateStatus.Stale;
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + dbx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return operationStatus;
        }
        public int Delete(int id)
        {
            T currentEntity = GetByExpression(ent => ent.Id == id).FirstOrDefault();
            _db.Set<T>().Remove(currentEntity);
            return _db.SaveChanges();
        }
    }
}
