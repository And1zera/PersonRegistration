using Microsoft.AspNetCore.Identity;
using PersonRegistration.Domain.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PersonRegistration.Domain.Services
{
    public class PasswordService : IPasswordService
    {
        public string EncryptPassword(string password)
        {
            var encodedValue = Encoding.UTF8.GetBytes(password);
            var encryptedPassword = HashAlgorithm.Create("SHA512").ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }

        public string PasswordGenerator()
        {
            int passwordSize = 6;
            string strings = "abcdefghijklmnozABCDEFGHIJKLMNOZ1234567890";

            StringBuilder strbld = new StringBuilder(100);
            Random random = new Random();
            while (0 < passwordSize--)
            {
                strbld.Append(strings[random.Next(strings.Length)]);
            }
            return strbld.ToString();
        }

        public bool ValidationPassword(string enteredPassword, string registeredPassword)
        {
            if (string.IsNullOrEmpty(registeredPassword))
                throw new NullReferenceException("Cadastre uma senha.");

            var encodedValue = Encoding.UTF8.GetBytes(enteredPassword);
            var encryptedPassword = HashAlgorithm.Create("SHA512").ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caractere in encryptedPassword)
            {
                sb.Append(caractere.ToString("X2"));
            }

            return sb.ToString() == registeredPassword;
        }
    }
}
