using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Application.Comandos;
using CleanArchitecture.Application.Comandos.Autenticar;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Application.Seguranca;
using CleanArchitecture.Domain.Clientes;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Framework;
using Xunit;

namespace CleanArchitecture.Application.Tests
{
    public class AutenticarUseCaseTests
    {
        private IClienteRepository clienteRepository;
        private ITokenProvider tokenProvider;

        public AutenticarUseCaseTests()
        {
            this.clienteRepository = Substitute.For<IClienteRepository>();

            this.clienteRepository.ObterPorCpf("123456789").Returns(Cliente.Carregar(Guid.NewGuid(), "Gustavo Martins Oliveira", "12345678",new Senha( "minh@senh@Forte")));

            this.tokenProvider = new JwtTokenProvider(new TokenConfigurations(){
                Audience="DbServer",
                Issuer="DbServer",
                Seconds=60
            }, new SigningConfigurations());
        }

        [Fact]
        public async void DeveDispararExceptionSeSenhaEstiverErradaNaAutenticacao()
        {
            IAutenticarUseCase autenticarUseCase = new AutenticarUseCase(this.clienteRepository,this.tokenProvider);
            var ex = await Assert.ThrowsAsync<UsuarioOuSenhaInvalidosException>(() =>
            {
                return autenticarUseCase.Execute("123456789", "minh@senh@");
            });

            Assert.Equal("Usuário/Senha inválidos.", ex.Message);
        }

        [Fact]
        public async void DeveConseguirGerarUmTokenDeAcessoParaOUsuario()
        {
            IAutenticarUseCase autenticarUseCase = new AutenticarUseCase(this.clienteRepository, this.tokenProvider);
            var result = await autenticarUseCase.Execute("123456789", "minh@senh@Forte");
            Assert.NotNull(result.TokenAcesso);
        }
    }
}
