using AutoMapper;
using PersonRegistration.Domain.DTOs;
using PersonRegistration.Domain.Entities;
using PersonRegistration.Domain.Interfaces.IRepository;
using PersonRegistration.Domain.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PersonRegistration.Domain.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IMapper _mapper;

        public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PersonDTO Add(PersonDTO dto)
        {
            if (dto == null)
                throw new Exception("not found");

            var entity = _mapper.Map<Person>(dto);

            entity.Id = Guid.NewGuid();
            entity.CreateAt = DateTime.Now.ToUniversalTime();
            entity.Status = true;
            entity.Password = EncryptPassword(dto.Password);

            _unitOfWork.PersonRepository.Add(entity);
            _unitOfWork.commit();

            return GetById(entity.Id);
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new Exception("Id está vazio");

            Person entity = _unitOfWork.PersonRepository.GetById(id);

            if (entity == null)
                throw new Exception("Pessoa não Existe");

            _unitOfWork.PersonRepository.Delete(entity);
            _unitOfWork.commit();

        }

        public PersonDTO GetById(Guid id)
        {
            Person entity = _unitOfWork.PersonRepository.GetById(id);

            if (entity == null)
                throw new Exception("Pessoa não Existe");

            PersonDTO personDTO = _mapper.Map<PersonDTO>(entity);

            personDTO.CreateAt = personDTO.CreateAt.ToLocalTime();

            return personDTO;
        }

        public List<PersonDTO> ListAll()
        {
            var list = _unitOfWork.PersonRepository.ListAll().ToList();

            if (list == null)
                throw new Exception("Listagem vazia");

            return _mapper.Map<List<PersonDTO>>(list);
        }

        public PersonDTO Update(PersonDTO dto)
        {
            if (dto == null)
                throw new Exception("not found");

            Person entity = _unitOfWork.PersonRepository.GetById(dto.Id);

            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.Address = dto.Address;
            entity.City = dto.City;
            entity.State = dto.State;
            entity.Phone = dto.Phone;

            _unitOfWork.PersonRepository.Update(entity);
            _unitOfWork.commit();

            return GetById(entity.Id);
        }

        public string EncryptPassword(string senha)
        {
            var encodedValue = Encoding.UTF8.GetBytes(senha);
            var encryptedPassword = HashAlgorithm.Create("SHA512").ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }

        //public bool VerifyPassword(string senhaDigitada, string senhaCadastrada)
        //{
        //    if (string.IsNullOrEmpty(senhaCadastrada))
        //        throw new NullReferenceException("Insira uma senha para verificação.");

        //    var encryptedPassword = _algorithm.ComputeHash(Encoding.UTF8.GetBytes(senhaDigitada));

        //    var sb = new StringBuilder();
        //    foreach (var caractere in encryptedPassword)
        //    {
        //        sb.Append(caractere.ToString("X2"));
        //    }

        //    return sb.ToString() == senhaCadastrada;
        //}
    }
}
