using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Application.Results;
using CleanArchitecture.Domain.Contas;

namespace CleanArchitecture.Application.Comandos.Debitar
{
    public class DebitarResult 
    {
        public DateTime DataTransacao { get; set; }
        public double ValorTransacao { get; set; }
        public double SaldoAtual { get; set; }

        public DebitarResult(Debito debito, double saldoAtual)
        {
            this.DataTransacao = debito.DataTransacao;
            this.ValorTransacao = debito.Valor;
            this.SaldoAtual = saldoAtual;
        }
    }
}
