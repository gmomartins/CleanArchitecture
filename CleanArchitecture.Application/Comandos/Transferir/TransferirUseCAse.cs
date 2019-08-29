using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Domain.Contas;
using CleanArchitecture.Domain.Seguranca;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Comandos.Transferir
{
    public class TransferirUseCAse : ITransferirUseCase
    {
        private readonly IContaCorrenteRepository contaRepository;
        private readonly IUsuarioAutenticado usuarioAutenticado;

        public TransferirUseCAse(IContaCorrenteRepository contaRepository, IUsuarioAutenticado usuarioAutenticado)
        {
            this.contaRepository = contaRepository;
            this.usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<TransferirResult> Execute(NumeroAgencia numeroAgenciaOrigem, NumeroConta numeroContaOrigem, DigitoConta digitoContaOrigem, NumeroAgencia numeroAgenciaDestino, NumeroConta numeroContaDestino, DigitoConta digitoContaDestino, Valor valor)
        {
            if (valor <= 0)
                throw new ValorInvalidoException();

            var contaDebito = await this.contaRepository.Obter(numeroAgenciaOrigem, numeroContaOrigem, digitoContaOrigem);

            if (contaDebito == null)
                throw new ContaNaoEncontradaException(numeroAgenciaOrigem, numeroContaOrigem, digitoContaOrigem);

            if (contaDebito.ClienteId != usuarioAutenticado.Id)
                throw new UsuarioLogadoNaoEhDonoDaContaException(contaDebito.Id);

            var contaCredito = await this.contaRepository.Obter(numeroAgenciaDestino, numeroContaDestino, digitoContaDestino);

            if (contaCredito == null)
                throw new ContaNaoEncontradaException(numeroAgenciaDestino, numeroContaDestino, digitoContaDestino);


            if (contaCredito.Id == contaDebito.Id)
                throw new ContaCreditoIgualContaDebitoException();

            contaDebito.Debitar(valor);

            Guid transacaoId = Guid.NewGuid();

            Debito debito = contaDebito.ObterUltimaTransacao() as Debito;

            debito.AtrelarTransacao(transacaoId);

            await this.contaRepository.SalvarTransacao(contaDebito, debito);

            contaCredito.Creditar(valor);

            Credito credito = contaCredito.ObterUltimaTransacao() as Credito;

            credito.AtrelarTransacao(debito.TransacaoId);

            await this.contaRepository.SalvarTransacao(contaCredito, credito);

            return new TransferirResult(valor, transacaoId, credito.DataTransacao, contaCredito, contaDebito);

        }
    }
}
