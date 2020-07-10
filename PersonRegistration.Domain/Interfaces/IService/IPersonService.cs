using PersonRegistration.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonRegistration.Domain.Interfaces.IService
{
    public interface IPersonService
    {
        PersonDTO Add(PersonDTO dto);
        PersonDTO GetById(Guid id);
        PersonDTO Update(PersonDTO dto);
        void Delete(Guid id);
        List<PersonDTO> ListAll();

        //List<PersonDTO> ListByFilter();
    }
}
