using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Application.Comandos.Creditar;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Domain.Contas;
using CleanArchitecture.Domain.Seguranca;
using Xunit;

namespace CleanArchitecture.Application.Tests
{
    public class CreditarUseCaseTests
    {
        private IContaCorrenteRepository contaRepository;
        private IUsuarioAutenticado usuarioAutenticado;
        private ContaCorrente contaFake ;

        public CreditarUseCaseTests()
        {
            contaRepository = Substitute.For<IContaCorrenteRepository>();
            
            usuarioAutenticado = Substitute.For<IUsuarioAutenticado>();
            usuarioAutenticado.Id.Returns(Guid.NewGuid());

            contaFake = ContaCorrente.Carregar(Guid.NewGuid(), usuarioAutenticado.Id, "123", "45678", "9", new LancamentoCollection());

            contaRepository.Obter(contaFake.Id).Returns(contaFake);
        }

        [Fact]
        public async void DeveConseguirEfetuarUmCreditoEmUmaContaValida()
        {
            var useCase = new CreditarUseCase(contaRepository, usuarioAutenticado);

            var result = await useCase.Execute(contaFake.Id, 50);

            Assert.Equal(50, result.SaldoAtual);
        }

        [Fact]
        public async void NaoDeveConseguirEfetuarUmCreditoEmUmaContaInexistente()
        {
            var useCase = new CreditarUseCase(contaRepository, usuarioAutenticado);

            await Assert.ThrowsAsync<ContaNaoEncontradaException>(() =>
            {
                return useCase.Execute(Guid.NewGuid(), 50);
            });
        }

        [Fact]
        public async void DeveDispararUmaExceptionSeValorDoCreditoForMenorOuIgualAZero() {

            var useCase = new CreditarUseCase(contaRepository, usuarioAutenticado);

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
        public async void NaoDevePermitirEfetuarCreditoSeUsuarioLogadoNaoForDonoDaConta()
        {
            IUsuarioAutenticado outroUsuarioAutenticado = Substitute.For<IUsuarioAutenticado>();
            outroUsuarioAutenticado.Id.Returns(Guid.NewGuid());

            var useCase = new CreditarUseCase(contaRepository, outroUsuarioAutenticado);

            await Assert.ThrowsAsync<UsuarioLogadoNaoEhDonoDaContaException>(() =>
            {
                return useCase.Execute(contaFake.Id, 50);
            });
        }
    }
}
