using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace PersonRegistration.Domain.Interfaces.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> ListAll();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T FindBy(Expression<Func<T, bool>> where);
        List<T> ListBy(Expression<Func<T, bool>> where);
        bool FindExist(Expression<Func<T, bool>> where);
        void Commit();
    }
}
