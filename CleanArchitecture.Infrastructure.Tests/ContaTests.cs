using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Domain.Contas;
using CleanArchitecture.Infrastructure.EntityFrameworkDataAccess;
using CleanArchitecture.Infrastructure.EntityFrameworkDataAccess.Repositorios;
using Xunit;

namespace CleanArchitecture.Infrastructure.Tests
{
    public class ContaTests : TestBase
    {
        private IContaCorrenteRepository repository;

        public ContaTests()
        {
            this.repository = new ContaCorrenteRepository(this.Context);
        }

        [Fact]
        public async void ConsegueSalvarUmaNovaContaNoDataBase()
        {
            string numeroAgencia = "789";
            string numeroConta = "456789";
            string digitoConta = "1";

            ContaCorrente conta = new ContaCorrente(Guid.NewGuid(), numeroAgencia, numeroConta, digitoConta);

            await repository.Salvar(conta);

            var contaObtidaDoDB = await repository.Obter(conta.Id);

            Assert.Equal(numeroAgencia, contaObtidaDoDB.NumeroAgencia);
            Assert.Equal(numeroConta, contaObtidaDoDB.NumeroConta);
            Assert.Equal(digitoConta, contaObtidaDoDB.DigitoConta);
        }

        [Fact]
        public async void ConsegueSalvarAdicionarUmDebitoNaContaSallva()
        {
            ContaCorrente conta = new ContaCorrente(Guid.NewGuid(), "123", "456789", "2");

            Debito debito = new Debito(conta.Id, 50);

            await repository.SalvarTransacao(conta, debito);

            var debitos = await repository.ObterDebitos(conta.Id);

            Assert.Single(debitos);

            Assert.Equal(50, debitos[0].Valor);
        }

        [Fact]
        public async void ConsegueSalvarAdicionarUmCreditoNaContaSalvaAsync()
        {
            ContaCorrente conta = new ContaCorrente(Guid.NewGuid(), "123", "456789", "2");

            Credito credito = new Credito(conta.Id, 50);

            await repository.SalvarTransacao(conta, credito);

            var creditos = await repository.ObterCreditos(conta.Id);

            Assert.Single(creditos);

            Assert.Equal(50, creditos[0].Valor);
        }


        [Fact]
        public async void ConsegueCarregarUmaContaSalvaNoDataBase()
        {
            string numeroAgencia = "789";
            string numeroConta = "456789";
            string digitoConta = "1";

            ContaCorrente conta = new ContaCorrente(Guid.NewGuid(), numeroAgencia, numeroConta, digitoConta);

            await repository.Salvar(conta);

            var contaObtidaDoDB = await repository.Obter(conta.Id);

            Assert.Equal(numeroAgencia, contaObtidaDoDB.NumeroAgencia);
            Assert.Equal(numeroConta, contaObtidaDoDB.NumeroConta);
            Assert.Equal(digitoConta, contaObtidaDoDB.DigitoConta);
        }

        [Fact]
        public async void DeveRetornarExceptionSeNaoEncontrarAConta()
        {
            Guid contaId = Guid.NewGuid();
            var ex = await Assert.ThrowsAsync<ContaNaoEncontradaException>(() =>
            {
                return this.repository.Obter(contaId);
            });

            Assert.Equal($"Conta '{contaId}' não encontrada.", ex.Message);

            string numeroAgencia = "555";
            string numeroConta = "456789";
            string digitoConta = "2";

            ex = await Assert.ThrowsAsync<ContaNaoEncontradaException>(() =>
            {
                return this.repository.Obter(numeroAgencia,numeroConta,digitoConta);
            });

            Assert.Equal($"Conta '{numeroConta}-{digitoConta}' não encontrada na agência '{numeroAgencia}'.", ex.Message);
        }

        [Fact]
        public async void DeveObterContaPeloNumeroAgencia_NumeroConta_DigitoConta()
        {
            string numeroAgencia = "123";
            string numeroConta = "456789";
            string digitoConta = "2";

            ContaCorrente conta = new ContaCorrente(Guid.NewGuid(), numeroAgencia, numeroConta, digitoConta);

            await repository.Salvar(conta);

             conta = await this.repository.Obter(numeroAgencia, numeroConta, digitoConta);

            Assert.Equal(numeroAgencia, conta.NumeroAgencia);
            Assert.Equal(numeroConta, conta.NumeroConta);
            Assert.Equal(digitoConta, conta.DigitoConta);
        }
    }
}
