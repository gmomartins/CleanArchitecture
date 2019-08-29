using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;
using Xunit;

namespace CleanArchitecture.Domain.Tests
{
    public class SenhaCriptografadaTests
    {
        [Fact]
        public void DeveCriptografarSenha()
        {
            SenhaCriptografada senhaCriptografada = new SenhaCriptografada("minh@senh@");

            Assert.Equal("622d20d0dc638e1eacb16e915465264163a39ec9386520b5307124ffef7be6facc7d1fe791a6ec6f60c970ae0ee536cfb9965282a878164a86fa8e5d71d637a4", senhaCriptografada.ToString());
        }
    }
}
