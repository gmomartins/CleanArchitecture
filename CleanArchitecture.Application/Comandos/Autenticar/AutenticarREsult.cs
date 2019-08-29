using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Comandos.Autenticar
{
    public class AutenticarResult
    {
        public Cpf Usuario { get; set; }
        public TokenAcesso TokenAcesso { get; set; }
    }
}
