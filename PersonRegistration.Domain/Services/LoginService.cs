using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PersonRegistration.Domain.DTOs;
using PersonRegistration.Domain.Interfaces.IRepository;
using PersonRegistration.Domain.Interfaces.IService;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PersonRegistration.Domain.Services
{
    public class LoginService : ILoginService
    {
        private IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        private IMapper _mapper;

        public LoginService(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _mapper = mapper;
        }

        public string Login(PersonDTO person)
        {
            var entity = _unitOfWork.PersonRepository.FindBy(x => person.Email == x.Email);

            if (entity == null)
                throw new Exception("Pessoa não castrada");

            var Validation = ValidationPassword(person.Password, entity.Password);

            if (Validation == false)
                throw new Exception("Senha Errada");

            var dto = _mapper.Map<PersonDTO>(entity);

            var tokenStr = GenerateJSONWebToken(dto);

            return tokenStr;
        }

        private string GenerateJSONWebToken(PersonDTO userInfo)
        {
            var vada = _config["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var Credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Name),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: Credentials
            );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }

        private bool ValidationPassword(string enteredPassword, string registeredPassword)
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
