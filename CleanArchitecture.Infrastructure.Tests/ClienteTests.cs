using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Domain.Clientes;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Infrastructure.EntityFrameworkDataAccess;
using CleanArchitecture.Infrastructure.EntityFrameworkDataAccess.Repositorios;
using Xunit;

namespace CleanArchitecture.Infrastructure.Tests
{
    public class ClienteTests : TestBase
    {
        private IClienteRepository repository;

        public ClienteTests()
        {
            repository = new ClienteRepository(this.Context);
        }

        [Fact]
        public async void ConsegueSalvarUmNovoClienteNoDataBase()
        {
            string nome = "Gustavo Martins Oliveira";
            string cpf = "11111111111";

            Cliente cliente = new Cliente(nome, cpf);

            cliente.AlterarSenha(Senha.Default);

            await repository.Salvar(cliente);


            var clienteObtidoDb = await repository.Obter(cliente.Id);

            Assert.Equal(nome, clienteObtidoDb.Nome);
            Assert.Equal(cpf, clienteObtidoDb.Cpf);
        }

        [Fact]
        public async void DeveRetornarExceptionSeNaoEncontrarOCliente()
        {
            Guid clienteId = Guid.NewGuid();
            var ex = await Assert.ThrowsAsync<ClienteNaoEncontradoException>(() =>
            {
                return this.repository.Obter(clienteId);
            });

            Assert.Equal($"Cliente '{clienteId}' não encontrado.", ex.Message);
        }
    }
}
