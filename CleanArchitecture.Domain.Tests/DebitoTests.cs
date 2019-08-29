using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.Contas;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class DebitoTests
    {
        [Fact]
        public void ConsegueCarregarUmObjetoDebito()
        {
            Guid id = Guid.NewGuid();
            Guid transacaoId = Guid.NewGuid();
            Guid contaId = Guid.NewGuid();
            DateTime dataTransacao = DateTime.UtcNow;
            double valor = 49.9;

            var debito = Debito.Carregar(id, transacaoId, contaId, dataTransacao, valor);

            Assert.Equal(id, debito.Id);
            Assert.Equal(transacaoId, debito.TransacaoId);
            Assert.Equal(contaId, debito.ContaId);
            Assert.Equal(dataTransacao, debito.DataTransacao);
            Assert.Equal<double>(valor, debito.Valor);
        }
    }
}
