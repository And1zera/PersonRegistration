using System;
using System.Collections.Generic;
using System.Text;

namespace PersonRegistration.Domain.Interfaces.IRepository
{
    public interface IPasswordService
    {
        bool ValidationPassword(string enteredPassword, string registeredPassword);
        string EncryptPassword(string password);
        string PasswordGenerator();
    }
}
