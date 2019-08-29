using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.ValueObjects
{
    public class TokenAcessoNaoInformadoException : DomainException
    {
        public TokenAcessoNaoInformadoException() : base("Token de acesso é obrigatório.")
        {
        }
    }
}
