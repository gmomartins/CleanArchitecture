using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Comandos.Autenticar;
using CleanArchitecture.Autenticacao.Api.Models;

namespace CleanArchitecture.Autenticacao.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<LoginResponse> Post([FromBody] LoginRequest loginRequest, [FromServices]IAutenticarUseCase autenticarUseCase)
        {
            var result = await autenticarUseCase.Execute(loginRequest.Usuario, loginRequest.Senha);

            return new LoginResponse() { 
                TokenAcesso=result.TokenAcesso                
            };
        }
    }
}