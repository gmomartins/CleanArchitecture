using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Contas
{
    public class Debito : IEntity, ITransacao
    {
        public Guid Id { get; private set; }
        public Guid TransacaoId { get; private set; }
        public Guid ContaId { get; private set; }
        public Valor Valor { get; private set; }
        public DateTime DataTransacao { get; private set; }

        private Debito() { }

        public Debito(Guid contaId, Valor valor) : this(contaId, valor, DateTime.UtcNow)
        {

        }

        public Debito(Guid contaId, Valor valor, DateTime dataTransacao)
        {
            this.Id = Guid.NewGuid();
            this.ContaId = contaId;
            this.Valor = valor;
            this.DataTransacao = dataTransacao;
        }

        public static Debito Carregar(Guid id, Guid transacaoId, Guid contaId, DateTime dataTransacao, Valor valor)
        {
            Debito debito = new Debito();
            debito.Id = id;
            debito.TransacaoId = transacaoId;
            debito.ContaId = contaId;
            debito.DataTransacao = dataTransacao;
            debito.Valor = valor;
            return debito;
        }

        public void AtrelarTransacao(Guid transacaoId)
        {
            this.TransacaoId = transacaoId;
        }

    }
}
