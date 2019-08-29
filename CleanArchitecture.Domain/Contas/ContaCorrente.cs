using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Contas
{
    public class ContaCorrente : IEntity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public Guid ClienteId { get; private set; }
        public NumeroAgencia NumeroAgencia { get; private set; }
        public NumeroConta NumeroConta { get; private set; }
        public DigitoConta DigitoConta { get; private set; }

        private LancamentoCollection transacoes;

        private ContaCorrente() { }

        public ContaCorrente(Guid clienteId,NumeroAgencia numeroAgencia, NumeroConta numeroConta, DigitoConta digitoConta)
        {
            this.Id = Guid.NewGuid();
            this.ClienteId = clienteId;
            this.NumeroAgencia = numeroAgencia;
            this.NumeroConta = numeroConta;
            this.DigitoConta = digitoConta;
            this.transacoes = new LancamentoCollection();
        }

        public void Creditar(Valor valor)
        {
            Credito credto = new Credito(this.Id, valor);
            this.transacoes.Adicionar(credto);
        }

        public ITransacao ObterUltimaTransacao()
        {
            return this.transacoes.ObterUltimaTransacao();
        }

        public void Debitar(Valor valor)
        {
            Valor saldoAtual = this.transacoes.ObterSaldo();
            if (saldoAtual < valor)
                throw new SaldoInsuficienteException();

            Debito debito = new Debito(this.Id, valor);

            this.transacoes.Adicionar(debito);
        }

        public Valor ObterSaldo()
        {
            return this.transacoes.ObterSaldo();
        }

        public static ContaCorrente Carregar(Guid id, Guid clienteId, NumeroAgencia numeroAgencia, NumeroConta numeroConta, DigitoConta digitoConta, LancamentoCollection transacoes)
        {
            var conta = new ContaCorrente();
            conta.NumeroAgencia = numeroAgencia;
            conta.NumeroConta = numeroConta;
            conta.DigitoConta = digitoConta;
            conta.Id = id;
            conta.ClienteId = clienteId;
            conta.transacoes = transacoes;
            return conta;
        }
    }
}