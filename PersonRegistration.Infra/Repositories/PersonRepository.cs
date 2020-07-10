using Microsoft.EntityFrameworkCore;
using PersonRegistration.Domain.Entities;
using PersonRegistration.Domain.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonRegistration.Infra.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(DbContext context) : base(context)
        {
        }
    }
}
