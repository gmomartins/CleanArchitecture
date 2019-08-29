using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.Contas;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class CreditoTests
    {
        [Fact]
        public void ConsegueCarregarUmObjetoCredito()
        {
            Guid id = Guid.NewGuid();
            Guid contaId = Guid.NewGuid();
            Guid transacaoId = Guid.NewGuid();
            DateTime dataTransacao = DateTime.UtcNow;
            double valor = 49.9;

            var credito = Credito.Carregar(id, transacaoId, contaId, dataTransacao, valor);

            Assert.Equal(id, credito.Id);
            Assert.Equal(contaId, credito.ContaId);
            Assert.Equal(dataTransacao, credito.DataTransacao);
            Assert.Equal<double>(valor, credito.Valor);
        }
    }
}
