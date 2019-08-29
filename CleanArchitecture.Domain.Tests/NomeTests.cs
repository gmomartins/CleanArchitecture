using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class NomeTests
    {
        [Fact]
        public void DeveConseguirCriarUmNome()
        {
            Nome nome = new Nome("Gustavo Martins Oliveira");

            Assert.Equal("Gustavo Martins Oliveira", nome);
        }

        [Fact]
        public void NaoDeveConseguirCriarUmNomeCasoOValorInformadoForNullOuBranco()
        {

            NomeNaoInformadoException ex = Assert.Throws<NomeNaoInformadoException>(() =>
            {
                Nome nome = new Nome(null);
            });

            Assert.Equal("Nome é obrigatório.", ex.Message);

            ex = Assert.Throws<NomeNaoInformadoException>(() =>
            {
                Nome nome = new Nome(String.Empty);
            });

            Assert.Equal("Nome é obrigatório.", ex.Message);
        }
    }
}
