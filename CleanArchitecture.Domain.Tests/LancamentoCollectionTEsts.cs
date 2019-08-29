using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.Contas;
using CleanArchitecture.Domain.ValueObjects;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class LancamentoCollectionTEsts
    {
        [Fact]
        public void DeveConseguirAdicionarUmaTransacao()
        {
            LancamentoCollection transacoes = new LancamentoCollection();

            Guid contaId = Guid.NewGuid();
            Valor valor = 50;

            transacoes.Adicionar(new Credito(contaId, valor));

            ITransacao transacao = transacoes.ObterUltimaTransacao();

            Assert.Equal(contaId, transacao.ContaId);
            Assert.Equal(valor, transacao.Valor);
        }

        [Fact]
        public void DeveConseguirObterTodasAsTransacoes()
        {
            LancamentoCollection transacoes = new LancamentoCollection();

            transacoes.Adicionar(new Credito(Guid.NewGuid(), 50));

            transacoes.Adicionar(new Debito(Guid.NewGuid(), 50));

            Assert.Equal(2, transacoes.ObterTransacoes().Count);
        }

        [Fact]
        public void DeveConseguirObterOSaldo()
        {
            LancamentoCollection transacoes = new LancamentoCollection();

            Guid contaId = Guid.NewGuid();

            transacoes.Adicionar(new Credito(contaId, 50));

            transacoes.Adicionar(new Credito(contaId, 50));

            transacoes.Adicionar(new Debito(contaId, 9.9));

            Valor saldoEsperado = 90.1;

            Assert.Equal(saldoEsperado, transacoes.ObterSaldo());
        }

        [Fact]
        public void DeveRetornarNullCasoNaoTenhaNenhumTransacao()
        {
            LancamentoCollection transacoes = new LancamentoCollection();

            ITransacao transacao = transacoes.ObterUltimaTransacao();

            Assert.Null(transacao);
        }

    }
}
