using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class DigitoContaTests
    {
        [Fact]
        public void DeveConseguirCriarUmDigitoConta()
        {
            DigitoConta numeroAgencia = new DigitoConta("1");

            Assert.Equal("1", numeroAgencia.ToString());
        }

        [Fact]
        public void NaoDeveConseguirCriarUmDigitoContaCasoOValorInformadoForNullOuBranco()
        {

            DigitoContaNaoInformadoException ex = Assert.Throws<DigitoContaNaoInformadoException>(() =>
            {
                DigitoConta digitoConta = new DigitoConta(null);
            });

            Assert.Equal("Digito da conta é obrigatório.", ex.Message);

            ex = Assert.Throws<DigitoContaNaoInformadoException>(() =>
            {
                DigitoConta digitoConta = new DigitoConta(String.Empty);
            });

            Assert.Equal("Digito da conta é obrigatório.", ex.Message);
        }
    }
}
