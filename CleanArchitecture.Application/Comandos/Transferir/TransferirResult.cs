using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Application.Results;
using CleanArchitecture.Domain.Contas;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Comandos.Transferir
{
    public class TransferirResult 
    {
        public DateTime DataTransacao { get; set; }
        public double ValorTransacao { get; set; }
        public Guid TransacaoId { get; set; }

        public ContaResult ContaDestinoResult { get; set; }
        public ContaResult ContaOrigemResult { get; set; }
        
        public TransferirResult(Valor valor, Guid transacaoId, DateTime dataTransacao, ContaCorrente contaCredito, ContaCorrente contaDebito) 
        {
            this.ValorTransacao = valor;
            this.TransacaoId = transacaoId;
            this.DataTransacao = dataTransacao;
            this.ContaDestinoResult = new ContaResult(contaCredito.NumeroAgencia, contaCredito.NumeroConta, contaCredito.DigitoConta);
            this.ContaOrigemResult = new ContaResult(contaDebito.NumeroAgencia, contaDebito.NumeroConta, contaDebito.DigitoConta);
        }
    }
}
