using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.Clientes;
using CleanArchitecture.Domain.Contas;

namespace CleanArchitecture.Application.Comandos.AbrirConta
{
    public class AbrirContaResult
    {
        public Cliente Cliente { get; set; }
        public ContaCorrente Conta { get; set; }
    }
}
