using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Autenticacao.Api.Models
{
    public class LoginResponse
    {
        public string TokenAcesso { get;  set; }
    }
}
