using Microsoft.EntityFrameworkCore;
using PersonRegistration.Domain.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PersonRegistration.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbContext _context;
        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public T FindBy(Expression<Func<T, bool>> where)
        {
            return _context.Set<T>().Where(where).FirstOrDefault();
        }

        public bool FindExist(Expression<Func<T, bool>> where)
        {
            return _context.Set<T>().Any(where);
        }

        public T GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> ListAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public List<T> ListBy(Expression<Func<T, bool>> where)
        {
            return _context.Set<T>().Where(where).ToList();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
