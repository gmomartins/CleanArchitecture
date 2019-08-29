using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Domain.Contas;
using CleanArchitecture.Domain.Seguranca;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Comandos.Debitar
{
    public class DebitarUseCase : IDebitarUseCase
    {
        private readonly IContaCorrenteRepository contaRepository;
        private readonly IUsuarioAutenticado usuarioAutenticado;

        public DebitarUseCase(IContaCorrenteRepository contaRepository, IUsuarioAutenticado usuarioAutenticado)
        {
            this.contaRepository = contaRepository;
            this.usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<DebitarResult> Execute(Guid contaId, Valor valor)
        {
            if (valor <= 0)
                throw new ValorInvalidoException();

            var conta = await this.contaRepository.Obter(contaId);

            if (conta == null)
                throw new ContaNaoEncontradaException(contaId);

            if (conta.ClienteId != usuarioAutenticado.Id)
                throw new UsuarioLogadoNaoEhDonoDaContaException(contaId);

            conta.Debitar(valor);

            Debito debito = conta.ObterUltimaTransacao() as Debito;

            await this.contaRepository.SalvarTransacao(conta, debito);

            return new DebitarResult(debito, conta.ObterSaldo());

        }
    }
}
