using System;
using System.Collections.Generic;
using System.Text;

namespace PersonRegistration.Domain.Interfaces.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository PersonRepository { get; }
        bool commit();
    }
}
