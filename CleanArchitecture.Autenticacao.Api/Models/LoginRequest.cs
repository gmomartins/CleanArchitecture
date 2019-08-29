using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Autenticacao.Api.Models
{
    public class LoginRequest
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}
