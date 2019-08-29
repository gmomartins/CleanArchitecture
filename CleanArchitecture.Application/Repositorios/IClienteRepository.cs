using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Clientes;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Repositorios
{
    public interface IClienteRepository
    {
        Task Salvar(Cliente cliente);
        Task<Cliente> Obter(Guid clienteId);
        Task<Cliente> ObterPorCpf(Cpf cpfCliente);
        Task Atualizar(Cliente cliente);
    }
}
