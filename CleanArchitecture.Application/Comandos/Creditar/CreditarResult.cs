using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Application.Results;
using CleanArchitecture.Domain.Contas;

namespace CleanArchitecture.Application.Comandos.Creditar
{
    public class CreditarResult
    {
        public double SaldoAtual { get; set; }
        public DateTime DataTransacao { get; set; }
        public double ValorTransacao { get; set; }

        public CreditarResult(Credito credito, double saldoAtual)
        {
            this.DataTransacao = credito.DataTransacao;
            this.ValorTransacao = credito.Valor;
            this.SaldoAtual = saldoAtual;
        }
    }
}
