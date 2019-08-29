using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class NumeroAgenciaTests
    {
        [Fact]
        public void DeveConseguirCriarUmNumerAgencia()
        {
            NumeroAgencia numeroAgencia = new NumeroAgencia("123");

            Assert.Equal("123", numeroAgencia.ToString());
        }

        [Fact]
        public void NaoDeveConseguirCriarUmNumeroAgenciaCasoOValorInformadoForNullOuBranco()
        {

            NumeroAgenciaNaoInformadoException ex = Assert.Throws<NumeroAgenciaNaoInformadoException>(() =>
             {
                 NumeroAgencia numeroAgencia = new NumeroAgencia(null);
             });

            Assert.Equal("Número da agência é obrigatório.", ex.Message);

            ex = Assert.Throws<NumeroAgenciaNaoInformadoException>(() =>
           {
               NumeroAgencia numeroAgencia = new NumeroAgencia(String.Empty);
           });

            Assert.Equal("Número da agência é obrigatório.", ex.Message);
        }
    }
}
