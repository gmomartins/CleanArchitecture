using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Domain.Clientes;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Infrastructure.EntityFrameworkDataAccess.Repositorios
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly Context context;

        public ClienteRepository(Context context)
        {
            this.context = context;
        }

        public async Task Atualizar(Cliente cliente)
        {
            var entity = new Entities.Cliente()
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,
                Senha = cliente.Senha
            };

            context.Clientes.Attach(entity);

            await context.SaveChangesAsync();
        }

        public async Task<Cliente> Obter(Guid clienteId)
        {
            var entity = await context.Clientes.FirstOrDefaultAsync(s => s.Id.Equals(clienteId));

            if (entity == null)
                throw new ClienteNaoEncontradoException(clienteId);

            return Cliente.Carregar(entity.Id, entity.Nome, entity.Cpf, entity.Senha);
        }

        public async Task<Cliente> ObterPorCpf(Cpf cpfCliente)
        {
            var entity = await context.Clientes.FirstOrDefaultAsync(s => s.Cpf.Equals(cpfCliente));

            if (entity == null)
                return null;

            return Cliente.Carregar(entity.Id, entity.Nome, entity.Cpf,entity.Senha);
        }

        public async Task Salvar(Cliente cliente)
        {
            var entity = new Entities.Cliente()
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,
                Senha = cliente.Senha
            };

            await context.Clientes.AddAsync(entity);
            await context.SaveChangesAsync();
        }

    }
}
