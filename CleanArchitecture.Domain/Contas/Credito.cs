using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Contas
{

    public class Credito : IEntity, ITransacao
    {
        public Guid Id { get; private set; }
        public Guid TransacaoId { get; private set; }
        public Guid ContaId { get; private set; }
        public Valor Valor { get; private set; }
        public DateTime DataTransacao { get ; private set; }

        private Credito() { }

        public Credito(Guid contaId, Valor valor):this(contaId, valor, DateTime.UtcNow)
        {
            
        }

        public Credito(Guid contaId, Valor valor, DateTime dataTransacao)
        {
            this.Id = Guid.NewGuid();
            this.TransacaoId = Guid.NewGuid();
            this.ContaId = contaId;
            this.Valor = valor;
            this.DataTransacao = dataTransacao;
        }

        public static Credito Carregar(Guid id, Guid transacaoId, Guid contaId, DateTime dataTransacao, Valor valor)
        {
            Credito credito = new Credito();
            credito.Id = id;
            credito.ContaId = contaId;
            credito.DataTransacao = dataTransacao;
            credito.Valor = valor;
            return credito;
        }

        public void AtrelarTransacao(Guid transacaoId)
        {
            this.TransacaoId = transacaoId;
        }
    }
}
