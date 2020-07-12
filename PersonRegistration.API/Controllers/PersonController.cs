using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonRegistration.Domain.DTOs;
using PersonRegistration.Domain.Interfaces.IService;
using System;
using System.Collections.Generic;

namespace PersonRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public ReturnOperation<PersonDTO> Add(PersonDTO personDTO)
        {
            ReturnOperation<PersonDTO> returnOp = new ReturnOperation<PersonDTO>();
            try
            {
                returnOp.Informacao = _personService.Add(personDTO);
                returnOp.Mensagens = new List<string>() { "Cadastro efetuado com sucesso!" };
            }
            catch (Exception ex)
            {
                returnOp.Mensagens = new List<string>() { "Não foi possivel Cadastrar Pessoa", "Erro: " + ex.Message };
                returnOp.Erro = true;
            }
            return returnOp;
        }

        
        [HttpGet("{id}")]
        public ReturnOperation<PersonDTO> GetById(Guid id)
        {
            ReturnOperation<PersonDTO> returnOp = new ReturnOperation<PersonDTO>();
            try
            {
                returnOp.Informacao = _personService.GetById(id);
                returnOp.Mensagens = new List<string>() { "Cadastro Encontrado com sucesso!" };
            }
            catch (Exception ex)
            {
                returnOp.Mensagens = new List<string>() { "Não foi possivel Encontrar Pessoa", "Erro: " + ex.Message };
                returnOp.Erro = true;
            }
            return returnOp;
        }
        [Authorize]
        [HttpPut]
        public ReturnOperation<PersonDTO> Update(PersonDTO personDTO)
        {
            ReturnOperation<PersonDTO> returnOp = new ReturnOperation<PersonDTO>();
            try
            {
                returnOp.Informacao = _personService.Update(personDTO);
                returnOp.Mensagens = new List<string>() { "Cadastro Atualizado com sucesso!" };
            }
            catch (Exception ex)
            {
                returnOp.Mensagens = new List<string>() { "Não foi possivel Atualizar Pessoa", "Erro: " + ex.Message };
                returnOp.Erro = true;
            }
            return returnOp;
        }
        [Authorize]
        [HttpDelete("{id}")]
        public ReturnOperation<string> Update(Guid id)
        {
            ReturnOperation<string> returnOp = new ReturnOperation<string>();
            try
            {
                _personService.Delete(id);
                returnOp.Mensagens = new List<string>() { "Cadastro Deletado com sucesso!" };
            }
            catch (Exception ex)
            {
                returnOp.Mensagens = new List<string>() { "Não foi possivel excluir Pessoa", "Erro: " + ex.Message };
                returnOp.Erro = true;
            }
            return returnOp;
        }

        [HttpGet("listAll")]
        public ReturnOperation<List<PersonDTO>> ListAll()
        {
            ReturnOperation<List<PersonDTO>> returnOp = new ReturnOperation<List<PersonDTO>>();
            try
            {
                returnOp.Informacao = _personService.ListAll();
                returnOp.Mensagens = new List<string>() { "Pessoas listadas com sucesso!" };
            }
            catch (Exception ex)
            {
                returnOp.Mensagens = new List<string>() { "Não foi possivel listar Pessoas", "Erro: " + ex.Message };
                returnOp.Erro = true;
            }
            return returnOp;
        }
    }
}
