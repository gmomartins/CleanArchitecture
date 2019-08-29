using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.Clientes;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class ClienteTests
    {

        [Fact]
        public void ConsegueCarregarUmObjetoCliente()
        {
            Guid id = Guid.NewGuid();
            string nome = "Gustavo Martins Oliveira";
            string cpf = "123456789-8";

            Cliente cliente = Cliente.Carregar(id, nome, cpf,"minh@senh@");

            Assert.Equal(id, cliente.Id);
            Assert.Equal(nome, cliente.Nome);
            Assert.Equal(cpf, cliente.Cpf);
        }

    }
}
