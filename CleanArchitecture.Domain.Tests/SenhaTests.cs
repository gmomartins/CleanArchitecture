using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class SenhaTests
    {
        [Fact]
        public void DeveConseguirCriarUmaSenha()
        {
            Senha nome = new Senha("Minh@Senh@&923");

            Assert.Equal("Minh@Senh@&923", nome);
        }

        [Fact]
        public void NaoDeveConseguirCriarUmaSenhaCasoOValorInformadoForNullOuBranco()
        {

            SenhaNaoInformadaException ex = Assert.Throws<SenhaNaoInformadaException>(() =>
            {
                Senha senha = new Senha(null);
            });

            Assert.Equal("Senha é obrigatória.", ex.Message);

            ex = Assert.Throws<SenhaNaoInformadaException>(() =>
            {
                Senha senha = new Senha(String.Empty);
            });

            Assert.Equal("Senha é obrigatória.", ex.Message);
        }
    }
}
