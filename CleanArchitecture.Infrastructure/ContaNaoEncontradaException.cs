using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infrastructure
{
    public class ContaNaoEncontradaException: InfrastructureException
    {
        private string numeroConta;
        private string digitoConta;

        public ContaNaoEncontradaException(Guid contaId):base($"Conta '{contaId}' não encontrada.")
        {

        }

        public ContaNaoEncontradaException(string numeroAgencia, string numeroConta, string digitoConta) : base($"Conta '{numeroConta}-{digitoConta}' não encontrada na agência '{numeroAgencia}'.")
        {

        }
    }
}
