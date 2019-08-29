using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.Contas;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Comandos.DetalharConta
{
    public class DetalharContaResult
    {
        public ContaCorrente ContaCorrente { get; set; }
        public double SaldoAtual { get; set; }
    }
}
