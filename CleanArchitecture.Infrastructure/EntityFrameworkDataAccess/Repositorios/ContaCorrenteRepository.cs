using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Domain.Contas;

namespace CleanArchitecture.Infrastructure.EntityFrameworkDataAccess.Repositorios
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly Context context;

        public ContaCorrenteRepository(Context context)
        {
            this.context = context;
        }

        public async Task<ContaCorrente> Obter(Guid contaId)
        {
            var entity = await context.Contas.FirstOrDefaultAsync(s => s.Id.Equals(contaId));

            if (entity == null)
                throw new ContaNaoEncontradaException(contaId);

            List<ITransacao> transacoes = new List<ITransacao>();

            var creditos = await this.ObterCreditos(entity.Id);

            foreach (var credito in creditos)
            {
                transacoes.Add(Credito.Carregar(credito.Id, credito.TransacaoId, credito.ContaId, credito.DataTransacao, credito.Valor));
            }

            var debitos = await this.ObterDebitos(entity.Id);

            foreach (var debito in debitos)
            {
                transacoes.Add(Debito.Carregar(debito.Id, debito.TransacaoId, debito.ContaId, debito.DataTransacao, debito.Valor));
            }

            transacoes.OrderBy(s => s.DataTransacao);

            LancamentoCollection transacaoCollection = new LancamentoCollection();

            transacaoCollection.Adicionar(transacoes);

            var conta = ContaCorrente.Carregar(entity.Id, entity.ClienteId, entity.NumeroAgencia, entity.NumeroConta, entity.DigitoConta, transacaoCollection);

            return conta;
        }

        public async Task Salvar(ContaCorrente conta)
        {
            var entity = new Entities.ContaCorrente()
            {
                Id = conta.Id,
                NumeroAgencia = conta.NumeroAgencia,
                NumeroConta = conta.NumeroConta,
                DigitoConta = conta.DigitoConta
            };

            await context.Contas.AddAsync(entity);
            await context.SaveChangesAsync();
        }


        public async Task SalvarTransacao(ContaCorrente conta, Debito debito)
        {
            var entity = new Entities.Debito()
            {
                ContaId = conta.Id,
                DataTransacao = debito.DataTransacao,
                Valor = debito.Valor
            };

            await context.Debitos.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task SalvarTransacao(ContaCorrente conta, Credito credito)
        {
            var entity = new Entities.Credito()
            {
                ContaId = conta.Id,
                DataTransacao = credito.DataTransacao,
                Valor = credito.Valor
            };

            await context.Creditos.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Credito>> ObterCreditos(Guid contaId)
        {
            var creditoEntities = await context.Creditos.Where(s => s.ContaId.Equals(contaId)).ToListAsync();

            List<Credito> creditos = new List<Credito>();

            if (creditoEntities != null)
            {
                foreach (var creditoEntity in creditoEntities)
                {
                    creditos.Add(Credito.Carregar(creditoEntity.Id, creditoEntity.TransacaoId, creditoEntity.ContaId, creditoEntity.DataTransacao, creditoEntity.Valor));
                }
            }
            return creditos;
        }

        public async Task<IList<Debito>> ObterDebitos(Guid contaId)
        {
            var debitoEntities = await context.Debitos.Where(s => s.ContaId.Equals(contaId)).ToListAsync();

            List<Debito> debitos = new List<Debito>();

            if (debitoEntities != null)
            {
                foreach (var creditoEntity in debitoEntities)
                {
                    debitos.Add(Debito.Carregar(creditoEntity.Id, creditoEntity.TransacaoId, creditoEntity.ContaId, creditoEntity.DataTransacao, creditoEntity.Valor));
                }
            }
            return debitos;
        }

        public async Task<ContaCorrente> Obter(string numeroAgencia, string numeroConta, string digitoConta)
        {
            var entity = await context.Contas.FirstOrDefaultAsync(s => s.NumeroAgencia.Equals(numeroAgencia) && s.NumeroConta.Equals(numeroConta) && s.DigitoConta.Equals(digitoConta));

            if (entity == null)
                throw new ContaNaoEncontradaException(numeroAgencia, numeroConta, digitoConta);

            List<ITransacao> transacoes = new List<ITransacao>();

            var creditos = await this.ObterCreditos(entity.Id);

            foreach (var credito in creditos)
            {
                transacoes.Add(Credito.Carregar(credito.Id, credito.TransacaoId, credito.ContaId, credito.DataTransacao, credito.Valor));
            }

            var debitos = await this.ObterDebitos(entity.Id);

            foreach (var debito in debitos)
            {
                transacoes.Add(Debito.Carregar(debito.Id, debito.TransacaoId, debito.ContaId, debito.DataTransacao, debito.Valor));
            }

            transacoes.OrderBy(s => s.DataTransacao);

            LancamentoCollection transacaoCollection = new LancamentoCollection();

            transacaoCollection.Adicionar(transacoes);

            var conta = ContaCorrente.Carregar(entity.Id, entity.ClienteId, entity.NumeroAgencia, entity.NumeroConta, entity.DigitoConta, transacaoCollection);

            return conta;
        }
    }
}
