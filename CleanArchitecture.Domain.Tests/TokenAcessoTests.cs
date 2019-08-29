using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class TokenAcessoTests
    {
        [Fact]
        public void DeveConseguirCriarUmTokenDeAcesso()
        {
            TokenAcesso tokenAcesso = new TokenAcesso("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

            Assert.Equal("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", tokenAcesso);
        }

        [Fact]
        public void NaoDeveConseguirCriarUmTokenDeAcessoCasoOValorInformadoForNullOuBranco()
        {

            TokenAcessoNaoInformadoException ex = Assert.Throws<TokenAcessoNaoInformadoException>(() =>
            {
                TokenAcesso tokenAcesso = new TokenAcesso(null);
            });

            Assert.Equal("Token de acesso é obrigatório.", ex.Message);

            ex = Assert.Throws<TokenAcessoNaoInformadoException>(() =>
            {
                TokenAcesso tokenAcesso = new TokenAcesso(String.Empty);
            });

            Assert.Equal("Token de acesso é obrigatório.", ex.Message);
        }
    }
}
