using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Application.Comandos.DetalharConta;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Domain.Contas;
using CleanArchitecture.Domain.Seguranca;
using Xunit;

namespace CleanArchitecture.Application.Tests
{
    public class DetalharContaUseCaseTests
    {
        private IContaCorrenteRepository contaRepository;
        private IUsuarioAutenticado usuarioAutenticado;
        private ContaCorrente contaFake;

        public DetalharContaUseCaseTests()
        {
            contaRepository = Substitute.For<IContaCorrenteRepository>();

            usuarioAutenticado = Substitute.For<IUsuarioAutenticado>();
            usuarioAutenticado.Id.Returns(Guid.NewGuid());

            contaFake = ContaCorrente.Carregar(Guid.NewGuid(), usuarioAutenticado.Id, "123", "45678", "9", new LancamentoCollection());

            contaRepository.Obter(contaFake.Id).Returns(contaFake);
        }

        [Fact]
        public async void ConsegueDetalharConta()
        {
            contaFake.Creditar(50);

            var useCase = new DetalharContaUseCase(contaRepository, usuarioAutenticado);

            var result = await useCase.Execute(contaFake.Id);

            Assert.Equal(50, result.SaldoAtual);
        }

        [Fact]
        public async void NaoDevePermitirDetalharAContaSeUsuarioLogadoNaoForDonoDaConta()
        {
            IUsuarioAutenticado outroUsuarioAutenticado = Substitute.For<IUsuarioAutenticado>();
            outroUsuarioAutenticado.Id.Returns(Guid.NewGuid());

            var useCase = new DetalharContaUseCase(contaRepository, outroUsuarioAutenticado);

            await Assert.ThrowsAsync<UsuarioLogadoNaoEhDonoDaContaException>(() =>
            {
                return useCase.Execute(contaFake.Id);
            });
        }
    }
}
