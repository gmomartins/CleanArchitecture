using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.ValueObjects
{
    public class NomeNaoInformadoException: DomainException
    {
        public NomeNaoInformadoException() : base("Nome é obrigatório.")
        {
        }
    }
}
