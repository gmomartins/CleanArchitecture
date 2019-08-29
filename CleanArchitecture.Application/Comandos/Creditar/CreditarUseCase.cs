using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Domain.Contas;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Domain.Seguranca;

namespace CleanArchitecture.Application.Comandos.Creditar
{
    public class CreditarUseCase : ICreditarUseCase
    {
        private IContaCorrenteRepository contaRepository;
        private readonly IUsuarioAutenticado usuarioAutenticado;

        public CreditarUseCase(IContaCorrenteRepository contaRepository, IUsuarioAutenticado usuarioAutenticado)
        {
            this.contaRepository = contaRepository;
            this.usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<CreditarResult> Execute(Guid contaId, Valor valor)
        {
            if (valor <= 0)
                throw new ValorInvalidoException();

            var conta = await this.contaRepository.Obter(contaId);

            if (conta == null)
                throw new ContaNaoEncontradaException(contaId);

            if (conta.ClienteId != usuarioAutenticado.Id)
                throw new UsuarioLogadoNaoEhDonoDaContaException(contaId);

            conta.Creditar(valor);

            Credito credito = conta.ObterUltimaTransacao() as Credito;

            await this.contaRepository.SalvarTransacao(conta, credito);

            return new CreditarResult(credito,  conta.ObterSaldo());
        }
    }
}
