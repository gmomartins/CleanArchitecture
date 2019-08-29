using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.ValueObjects
{
    public class CpfInvalidoException : DomainException
    {
        public CpfInvalidoException(string cpf) : base($"Cpf '{cpf}' é inválido.")
        {

        }
    }
}
