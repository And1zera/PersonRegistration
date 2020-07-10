using PersonRegistration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonRegistration.Domain.Interfaces.IRepository
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
    }
}
