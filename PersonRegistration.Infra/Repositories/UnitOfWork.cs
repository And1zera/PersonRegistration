using Microsoft.EntityFrameworkCore;
using PersonRegistration.Domain.Interfaces.IRepository;
using System;

namespace PersonRegistration.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        public IPersonRepository PersonRepository { get; set; }

        public UnitOfWork(DbContext context)
        {
            _context = context;
            PersonRepository = new PersonRepository(_context);
        }
        public bool commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
