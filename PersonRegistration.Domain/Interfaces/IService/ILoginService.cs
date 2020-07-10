using PersonRegistration.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonRegistration.Domain.Interfaces.IService
{
    public interface ILoginService
    {
        string Login(PersonDTO person);
    }
}
