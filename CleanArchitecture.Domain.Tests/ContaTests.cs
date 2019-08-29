using System;
using CleanArchitecture.Domain.Contas;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class ContaTests
    {
        [Fact]
        public void ConsegueDebitar100ReaisDaContaCorrente()
        {
            ContaCorrente conta = new ContaCorrente(Guid.NewGuid(),"123", "45678", "9");

            conta.Creditar(100);

            Assert.Equal(100, conta.ObterSaldo());

            conta.Debitar(50);

            Assert.Equal(50, conta.ObterSaldo());
        }

        [Fact]
        public void NaoDeveDebitarCasoAContaCorrenteNaoTenhaSaldo()
        {
            ContaCorrente conta = new ContaCorrente(Guid.NewGuid(),"123", "45678", "9");

            conta.Creditar(50);

            Assert.Throws<SaldoInsuficienteException>(() =>
            {
                conta.Debitar(100);
            });
        }

        [Fact]
        public void ConsegueCreditar100ReaisNaContaCorrente()
        {
            ContaCorrente conta = new ContaCorrente(Guid.NewGuid(),"123", "45678", "9");

            Assert.Equal(0, conta.ObterSaldo());

            conta.Creditar(100);

            Assert.Equal(100, conta.ObterSaldo());
        }

        [Fact]
        public void DeveConseguirCarregarUmaConta()
        {
            Guid contaId = Guid.NewGuid();
            Guid clienteId=Guid.NewGuid();
            LancamentoCollection transacoes = new LancamentoCollection();
            transacoes.Adicionar(new Credito(contaId, 50));

            ContaCorrente conta = ContaCorrente.Carregar(contaId, clienteId, "123", "45678", "9", transacoes);

            Assert.Equal(contaId, conta.Id);

            Assert.Equal(clienteId, conta.ClienteId);

            Assert.Equal("123", conta.NumeroAgencia.ToString());

            Assert.Equal("45678", conta.NumeroConta.ToString());

            Assert.Equal("9", conta.DigitoConta.ToString());

            Assert.Equal(50, conta.ObterSaldo());
        }

        [Fact]
        public void ConsegueObterUltimaTransacao()
        {
            ContaCorrente conta = new ContaCorrente(Guid.NewGuid(),"123", "45678", "9");

            conta.Creditar(50);

            ITransacao transacao = conta.ObterUltimaTransacao();

            Assert.IsType<Credito>(transacao);

            Assert.Equal(50, transacao.Valor);
        }
    }
}
