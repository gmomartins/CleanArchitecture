using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Application.Comandos.Debitar;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Domain.Contas;
using CleanArchitecture.Domain.Seguranca;
using Xunit;

namespace CleanArchitecture.Application.Tests
{
    public class DebitarUseCaseTests
    {
        private IContaCorrenteRepository contaRepository;
        private IUsuarioAutenticado usuarioAutenticado;
        private ContaCorrente contaFake;

        public DebitarUseCaseTests()
        {
            contaRepository = Substitute.For<IContaCorrenteRepository>();

            usuarioAutenticado = Substitute.For<IUsuarioAutenticado>();
            usuarioAutenticado.Id.Returns(Guid.NewGuid());

            contaFake =  ContaCorrente.Carregar(Guid.NewGuid(), usuarioAutenticado.Id, "123", "45678", "9", new LancamentoCollection());

            contaRepository.Obter(contaFake.Id).Returns(contaFake);

        }

        [Fact]
        public async void DeveConseguirEfetuarUmDebitoEmUmaContaValida()
        {
            var useCase = new DebitarUseCase(contaRepository, usuarioAutenticado);

            contaFake.Creditar(100);

            var result = await useCase.Execute(contaFake.Id, 45.9);

            Assert.Equal(54.1, result.SaldoAtual);
        }

        [Fact]
        public async void NaoDeveConseguirEfetuarUmDebitoEmUmaContaInexistente()
        {
            var useCase = new DebitarUseCase(contaRepository, usuarioAutenticado);
            Guid contaId = Guid.NewGuid();

            await Assert.ThrowsAsync<ContaNaoEncontradaException>(() =>
            {
                return useCase.Execute(contaId, 45.9);
            });
        }

        [Fact]
        public async void DeveDispararUmaExceptionSeValorDoDebitoForMenorOuIgualAZero()
        {

            var useCase = new DebitarUseCase(contaRepository, usuarioAutenticado);

            await Assert.ThrowsAsync<ValorInvalidoException>(() =>
            {
                return useCase.Execute(Guid.NewGuid(), 0);
            });

            await Assert.ThrowsAsync<ValorInvalidoException>(() =>
            {
                return useCase.Execute(Guid.NewGuid(), -50);
            });
        }

        [Fact]
        public async void NaoDevePermitirEfetuarDebitoSeUsuarioLogadoNaoForDonoDaConta()
        {
            IUsuarioAutenticado outroUsuarioAutenticado = Substitute.For<IUsuarioAutenticado>();
            outroUsuarioAutenticado.Id.Returns(Guid.NewGuid());

            var useCase = new DebitarUseCase(contaRepository, outroUsuarioAutenticado);

            await Assert.ThrowsAsync<UsuarioLogadoNaoEhDonoDaContaException>(() =>
            {
                return useCase.Execute(contaFake.Id, 50);
            });
        }
    }
}
