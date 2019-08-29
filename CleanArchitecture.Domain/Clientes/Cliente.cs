using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Clientes
{
    [Serializable]
    public class Cliente : IEntity
    {
        public Guid Id { get; set; }

        public Nome Nome { get; private set; }
        public Cpf Cpf { get; private set; }
        public SenhaCriptografada Senha { get; private set; }

        private Cliente() { }

        public Cliente(Nome nome, Cpf cpf)
        {
            this.Id = Guid.NewGuid();
            this.Nome = nome;
            this.Cpf = cpf;
        }

        public static Cliente Carregar(Guid id, Nome nome, Cpf cpf, SenhaCriptografada senha)
        {
            Cliente cliente = new Cliente();
            cliente.Id = id;
            cliente.Nome = nome;
            cliente.Cpf = cpf;
            cliente.Senha = senha;
            return cliente;
        }

        public void AlterarSenha(SenhaCriptografada senhaCriptografada)
        {
            this.Senha = senhaCriptografada;
        }
    }
}
