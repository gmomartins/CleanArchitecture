using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.ValueObjects
{
 public   class SenhaNaoInformadaException: DomainException
    {
        public SenhaNaoInformadaException():base("Senha é obrigatória.")
        {

        }
    }
}
