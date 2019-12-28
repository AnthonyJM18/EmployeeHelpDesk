/* Program Name : IRepository
 * Author       : Anthony Merante
 * Date         : October 23, 2019
 * Purpose      : Interface for repository methods
 */


using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HelpdeskDAL
{
    /*Interface that contains all methods needed to perform CRUD operations */
    public interface IRepository<T>
    {
        List<T> GetAll();
        List<T> GetByExpression(Expression<Func<T, bool>> match);
        T Add(T entity);
        UpdateStatus Update(T entity);
        int Delete(int i);
    }
}
