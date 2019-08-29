using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Domain.Seguranca;

namespace CleanArchitecture.Application.Comandos.DetalharConta
{
    public class DetalharContaUseCase : IDetalharContaUseCase
    {
        private readonly IContaCorrenteRepository contaRepository;
        private readonly IUsuarioAutenticado usuarioAutenticado;

        public DetalharContaUseCase(IContaCorrenteRepository contaRepository, IUsuarioAutenticado usuarioAutenticado)
        {
            this.contaRepository = contaRepository;
            this.usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<DetalharContaResult> Execute(Guid contaId)
        {
            var contaCorrente = await this.contaRepository.Obter(contaId);

            if (contaCorrente.ClienteId != usuarioAutenticado.Id)
                throw new UsuarioLogadoNaoEhDonoDaContaException(contaId);

            return new DetalharContaResult()
            {
                ContaCorrente = contaCorrente,
                SaldoAtual=contaCorrente.ObterSaldo()
            };
        }
    }
}
