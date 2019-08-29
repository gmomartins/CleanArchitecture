using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CleanArchitecture.Domain.Contas
{
    public class SaldoInsuficienteException : DomainException
    {
        public SaldoInsuficienteException():base("Conta não possuí saldo para efetuar esta operação.")
        {
        }
    }
}
