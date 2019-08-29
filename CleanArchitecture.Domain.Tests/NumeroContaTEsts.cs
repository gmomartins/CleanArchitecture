using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class NumeroContaTEsts
    {
        [Fact]
        public void DeveConseguirCriarUmNumerConta()
        {
            NumeroConta numeroConta= new NumeroConta("123");

            Assert.Equal("123", numeroConta.ToString());
        }

        [Fact]
        public void NaoDeveConseguirCriarUmNumeroContaCasoOValorInformadoForNullOuBranco()
        {

            NumeroContaNaoInformadoException ex = Assert.Throws<NumeroContaNaoInformadoException>(() =>
            {
                NumeroConta numeroConta = new NumeroConta(null);
            });

            Assert.Equal("Número da conta é obrigatório.", ex.Message);

            ex = Assert.Throws<NumeroContaNaoInformadoException>(() =>
            {
                NumeroConta numeroConta = new NumeroConta(String.Empty);
            });

            Assert.Equal("Número da conta é obrigatório.", ex.Message);
        }
    }
}
