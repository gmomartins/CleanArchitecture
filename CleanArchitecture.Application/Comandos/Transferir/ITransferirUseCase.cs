using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Contas;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Comandos.Transferir
{
    public interface ITransferirUseCase
    {
        Task<TransferirResult> Execute(NumeroAgencia numeroAgenciaOrigem, NumeroConta numeroContaOrigem, DigitoConta digitoContaOrigem, NumeroAgencia numeroAgenciaDestino, NumeroConta numeroContaDestino, DigitoConta digitoContaDestino, Valor valor);
    }
}
