using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CleanArchitecture.Application
{
    public class ContaNaoEncontradaException : ApplicationException
    {

        public ContaNaoEncontradaException(Guid contaId) : base($"Conta '{contaId}' não encontrada.") { }

        public ContaNaoEncontradaException(string numeroAgencia, string numeroConta, string digitoConta):base($"Conta '{numeroConta}-{digitoConta}' não encontrada na agência '{numeroAgencia}'.")
        {

        }

    }
}
