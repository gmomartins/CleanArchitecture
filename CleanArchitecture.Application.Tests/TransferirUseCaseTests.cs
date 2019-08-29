using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Application.Comandos.Transferir;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Domain.Contas;
using CleanArchitecture.Domain.Seguranca;
using Xunit;

namespace CleanArchitecture.Application.Tests
{
    public class TransferirUseCaseTests
    {
        private IContaCorrenteRepository contaRepository;
        private IUsuarioAutenticado usuarioAutenticado;
        private ContaCorrente contaOrigemFake;
        private ContaCorrente contaDestinoFake;

        public TransferirUseCaseTests()
        {
            contaRepository = Substitute.For<IContaCorrenteRepository>();

            usuarioAutenticado = Substitute.For<IUsuarioAutenticado>();
            usuarioAutenticado.Id.Returns(Guid.NewGuid());

            contaOrigemFake = ContaCorrente.Carregar(Guid.NewGuid(), usuarioAutenticado.Id, "123", "45678", "9", new LancamentoCollection());

            contaDestinoFake = ContaCorrente.Carregar(Guid.NewGuid(), Guid.NewGuid(), "987", "65432", "2", new LancamentoCollection());
        }

        [Fact]
        public async void NaoDeveConseguirEfetuarUmaTransferenciaSeContaDeOrigemNaoExistir()
        {
            var useCase = new TransferirUseCAse(contaRepository, usuarioAutenticado);
            var contaId = Guid.NewGuid();

            var ex = await Assert.ThrowsAsync<ContaNaoEncontradaException>(() =>
            {
                return useCase.Execute(contaOrigemFake.NumeroAgencia,
                    contaOrigemFake.NumeroConta,
                    contaOrigemFake.DigitoConta,
                    contaDestinoFake.NumeroAgencia,
                    contaDestinoFake.NumeroConta,
                    contaDestinoFake.DigitoConta,
                    50);
            });

            Assert.Equal($"Conta '{contaOrigemFake.NumeroConta}-{contaOrigemFake.DigitoConta}' não encontrada na agência '{contaOrigemFake.NumeroAgencia}'.", ex.Message);

        }

        [Fact]
        public async void NaoDeveConseguirEfetuarUmaTransferenciaSeContaDeDestinoNaoExistir()
        {
            var useCase = new TransferirUseCAse(contaRepository, usuarioAutenticado);
            var contaId = Guid.NewGuid();

            this.contaRepository.Obter(contaOrigemFake.NumeroAgencia, contaOrigemFake.NumeroConta, contaOrigemFake.DigitoConta).Returns(contaOrigemFake);

            var ex = await Assert.ThrowsAsync<ContaNaoEncontradaException>(() =>
            {
                return useCase.Execute(contaOrigemFake.NumeroAgencia,
                    contaOrigemFake.NumeroConta,
                    contaOrigemFake.DigitoConta,
                    contaDestinoFake.NumeroAgencia,
                    contaDestinoFake.NumeroConta,
                    contaDestinoFake.DigitoConta,
                    50);
            });

            Assert.Equal($"Conta '{contaDestinoFake.NumeroConta}-{contaDestinoFake.DigitoConta}' não encontrada na agência '{contaDestinoFake.NumeroAgencia}'.", ex.Message);
        }

        [Fact]
        public async void NaoDeveConseguirEfetuarUmaTransferenciaEntreAMesmaConta()
        {
            var useCase = new TransferirUseCAse(contaRepository, usuarioAutenticado);
            var contaId = Guid.NewGuid();

            this.contaRepository.Obter(contaOrigemFake.NumeroAgencia, contaOrigemFake.NumeroConta, contaOrigemFake.DigitoConta).Returns(contaOrigemFake);
            this.contaRepository.Obter(contaDestinoFake.NumeroAgencia, contaDestinoFake.NumeroConta, contaDestinoFake.DigitoConta).Returns(contaOrigemFake);

            var ex = await Assert.ThrowsAsync<ContaCreditoIgualContaDebitoException>(() =>
            {
                return useCase.Execute(contaOrigemFake.NumeroAgencia,
                    contaOrigemFake.NumeroConta,
                    contaOrigemFake.DigitoConta,
                    contaDestinoFake.NumeroAgencia,
                    contaDestinoFake.NumeroConta,
                    contaDestinoFake.DigitoConta,
                    50);
            });

            Assert.Equal(ex.Message, $"Conta de destino não pode ser a conta de origem.");
        }

        [Fact]
        public async void DeveConseguirFazerTransferenciaEntreContas()
        {
            var useCase = new TransferirUseCAse(contaRepository, usuarioAutenticado);
            var contaId = Guid.NewGuid();

            contaOrigemFake.Creditar(100);

            this.contaRepository.Obter(contaOrigemFake.NumeroAgencia, contaOrigemFake.NumeroConta, contaOrigemFake.DigitoConta).Returns(contaOrigemFake);
            this.contaRepository.Obter(contaDestinoFake.NumeroAgencia, contaDestinoFake.NumeroConta, contaDestinoFake.DigitoConta).Returns(contaDestinoFake);

            var result = await useCase.Execute(contaOrigemFake.NumeroAgencia,
                contaOrigemFake.NumeroConta,
                contaOrigemFake.DigitoConta,
                contaDestinoFake.NumeroAgencia,
                contaDestinoFake.NumeroConta,
                contaDestinoFake.DigitoConta,
                50);

            Assert.NotNull(result.ContaDestinoResult);
            Assert.NotNull(result.ContaDestinoResult);
            Assert.NotNull(result.TransacaoId.ToString());
            Assert.Equal(50, result.ValorTransacao);
        }

        [Fact]
        public async void DeveDispararUmaExceptionSeValorDaTransferenciaForMenorOuIgualAZero()
        {
            var useCase = new TransferirUseCAse(contaRepository, usuarioAutenticado);
            var contaId = Guid.NewGuid();

            contaOrigemFake.Creditar(100);

            this.contaRepository.Obter(contaOrigemFake.NumeroAgencia, contaOrigemFake.NumeroConta, contaOrigemFake.DigitoConta).Returns(contaOrigemFake);
            this.contaRepository.Obter(contaDestinoFake.NumeroAgencia, contaDestinoFake.NumeroConta, contaDestinoFake.DigitoConta).Returns(contaDestinoFake);

            await Assert.ThrowsAsync<ValorInvalidoException>(() =>
            {
                return useCase.Execute(contaOrigemFake.NumeroAgencia,
                contaOrigemFake.NumeroConta,
                contaOrigemFake.DigitoConta,
                contaDestinoFake.NumeroAgencia,
                contaDestinoFake.NumeroConta,
                contaDestinoFake.DigitoConta,
                0);
            });

            await Assert.ThrowsAsync<ValorInvalidoException>(() =>
            {
                return useCase.Execute(contaOrigemFake.NumeroAgencia,
                contaOrigemFake.NumeroConta,
                contaOrigemFake.DigitoConta,
                contaDestinoFake.NumeroAgencia,
                contaDestinoFake.NumeroConta,
                contaDestinoFake.DigitoConta,
                -50);
            });
        }

        [Fact]
        public async void NaoDevePermitirEfetuarTransferenciaSeUsuarioLogadoNaoForDonoDaConta()
        {
            IUsuarioAutenticado outroUsuarioAutenticado = Substitute.For<IUsuarioAutenticado>();
            outroUsuarioAutenticado.Id.Returns(Guid.NewGuid());

            var useCase = new TransferirUseCAse(contaRepository, outroUsuarioAutenticado);
            var contaId = Guid.NewGuid();

            contaOrigemFake.Creditar(100);

            this.contaRepository.Obter(contaOrigemFake.NumeroAgencia, contaOrigemFake.NumeroConta, contaOrigemFake.DigitoConta).Returns(contaOrigemFake);
            this.contaRepository.Obter(contaDestinoFake.NumeroAgencia, contaDestinoFake.NumeroConta, contaDestinoFake.DigitoConta).Returns(contaDestinoFake);

            await Assert.ThrowsAsync<UsuarioLogadoNaoEhDonoDaContaException>(() =>
            {
                return useCase.Execute(contaOrigemFake.NumeroAgencia,
                contaOrigemFake.NumeroConta,
                contaOrigemFake.DigitoConta,
                contaDestinoFake.NumeroAgencia,
                contaDestinoFake.NumeroConta,
                contaDestinoFake.DigitoConta,
                50);
            });
        }
    }
}
