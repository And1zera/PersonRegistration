using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
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
        private IPasswordService _passwordService;
        private IEmailService _emailService;

        public LoginService(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IPasswordService passwordService, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _mapper = mapper;
            _passwordService = passwordService;
            _emailService = emailService;
        }

        public string Login(PersonDTO person)
        {
            var entity = _unitOfWork.PersonRepository.FindBy(x => person.Email == x.Email);

            if (entity == null)
                throw new Exception("Pessoa não castrada");

            var Validation = _passwordService.ValidationPassword(person.Password, entity.Password);

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

        public void PasswordRecovery(string personEmail)
        {
            var person = _unitOfWork.PersonRepository.FindBy(x => x.Email.ToLower() == personEmail.ToLower());

            if (person == null)
                throw new Exception("Email não cadatrado no sistema");

            var newPass = _passwordService.PasswordGenerator();
            var newPassCrypt = _passwordService.EncryptPassword(newPass);

            person.Password = newPassCrypt;

            _unitOfWork.PersonRepository.Update(person);
            _unitOfWork.commit();

            EmailDTO email = new EmailDTO()
            {
                Name = person.Name,
                Email = person.Email,
                Subject = "Recuperação de senha",
                Body = "Sua Nova senha : " + newPass
            };

            _emailService.sendEmail(email);
        }
    }
}
