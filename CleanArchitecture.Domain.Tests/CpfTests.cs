using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class CpfTests
    {
        [Fact]
        public void DeveConseguirCriarUmCpf()
        {
            Cpf cpf = new Cpf("123");

            Assert.Equal("123", cpf.ToString());
        }

        [Fact]
        public void NaoDeveConseguirCriarUmCpfCasoOValorInformadoForNullOuBranco()
        {

            CpfInvalidoException ex = Assert.Throws<CpfInvalidoException>(() =>
            {
                Cpf numeroConta = new Cpf(null);
            });

            Assert.Equal("Cpf '' é inválido.", ex.Message);

            ex = Assert.Throws<CpfInvalidoException>(() =>
            {
                Cpf numeroConta = new Cpf(String.Empty);
            });

            Assert.Equal("Cpf '' é inválido.", ex.Message);
        }
    }
}
