using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Application.Comandos.AbrirConta;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Domain.Clientes;
using Xunit;

namespace CleanArchitecture.Application.Tests
{
    public class AbrirContaUseCaseTests
    {
        private IContaCorrenteRepository contaRepository;
        private IClienteRepository clienteRepository;

        public AbrirContaUseCaseTests()
        {
            this.contaRepository = Substitute.For<IContaCorrenteRepository>();
            this.clienteRepository = Substitute.For<IClienteRepository>();
        }

        [Fact]
        public async void DeveConseguirAbrirContaCasoSejaUmNovoCliente()
        {
            AbrirContaUseCase useCase = new AbrirContaUseCase(clienteRepository, contaRepository);

            string nome = "Gustavo Martins Oliveira";
            string cpf = "123456789";
            string numeroAgencia = "123";

            var result = await useCase.Execute(nome, cpf, numeroAgencia);

            Assert.Equal(nome, result.Cliente.Nome);
            Assert.Equal(cpf, result.Cliente.Cpf);
            Assert.Equal(numeroAgencia, result.Conta.NumeroAgencia);
            Assert.NotEmpty(result.Conta.NumeroConta.ToString());
            Assert.NotEmpty(result.Conta.DigitoConta.ToString());
        }

        [Fact]
        public async void NaoDeveAbrirContaCasoClienteJaPossuaUmaConta()
        {
            AbrirContaUseCase useCase = new AbrirContaUseCase(clienteRepository, contaRepository);

            clienteRepository.ObterPorCpf("123456789").Returns(Cliente.Carregar(Guid.NewGuid(), "Gustavo Martins Oliveira", "123456789", "minh@senh@"));

            string nome = "Gustavo Martins Oliveira";
            string cpf = "123456789";
            string numeroAgencia = "222";

            var ex = await Assert.ThrowsAsync<ClienteJaPossuiContaException>(() =>
            {
                return useCase.Execute(nome, cpf, numeroAgencia);
            });

            Assert.Equal("Cliente 'Gustavo Martins Oliveira' já possuí conta.", ex.Message);
        }
    }
}
