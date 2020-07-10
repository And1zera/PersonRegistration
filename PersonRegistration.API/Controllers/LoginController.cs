using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PersonRegistration.Domain.DTOs;
using PersonRegistration.Domain.Interfaces.IService;
using System;
using System.Collections.Generic;

namespace PersonRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private ILoginService _loginService;

        public LoginController(IConfiguration config, ILoginService loginService)
        {
            _config = config;
            _loginService = loginService;
        }

        [HttpGet]
        public ReturnOperation<string> Login(PersonDTO person)
        {
            ReturnOperation<string> returnOp = new ReturnOperation<string>();
            try
            {
                returnOp.Informacao = _loginService.Login(person);
                returnOp.Mensagens = new List<string>() { "Logado com sucesso!" };
            }
            catch (Exception ex)
            {
                returnOp.Mensagens = new List<string>() { "Não foi possivel Logar", "Erro: " + ex.Message };
                returnOp.Erro = true;
            }
            return returnOp;
        }
    }
}
