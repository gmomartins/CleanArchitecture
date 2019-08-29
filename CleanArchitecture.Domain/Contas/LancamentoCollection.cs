using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Contas
{
    public sealed class LancamentoCollection
    {
        private readonly IList<ITransacao> transacoes;

        public LancamentoCollection()
        {
            transacoes = new List<ITransacao>();
        }

        public IReadOnlyCollection<ITransacao> ObterTransacoes()
        {
            IReadOnlyCollection<ITransacao> transacoes = new ReadOnlyCollection<ITransacao>(this.transacoes);
            return transacoes;
        }

        public ITransacao ObterUltimaTransacao()
        {
            if (transacoes.Count > 0)
                return transacoes[transacoes.Count - 1];

            return default(ITransacao);
        }

        public void Adicionar(ITransacao transacao)
        {
            transacoes.Add(transacao);
        }

        public void Adicionar(IList<ITransacao> transacoes)
        {
            if (transacoes != null && transacoes.Count > 0)
            {
                foreach (var transacao in transacoes)
                {
                    this.transacoes.Add(transacao);
                }                
            }
        }

        public Valor ObterSaldo()
        {
            Valor total = 0;

            foreach (ITransacao transacao in transacoes)
            {
                if (transacao is Debito)
                    total = total - transacao.Valor;

                if (transacao is Credito)
                    total = total + transacao.Valor;
            }

            return total;
        }
    }
}
