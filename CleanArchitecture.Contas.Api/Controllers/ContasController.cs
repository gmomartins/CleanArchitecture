using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Contas.Api.Models;
using CleanArchitecture.Application.Comandos.AbrirConta;
using CleanArchitecture.Application.Comandos.Creditar;
using CleanArchitecture.Application.Comandos.Debitar;
using CleanArchitecture.Application.Comandos.DetalharConta;
using CleanArchitecture.Application.Comandos.Transferir;
using CleanArchitecture.Domain.Contas;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Framework;

namespace CleanArchitecture.Contas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ContasController : ControllerBase
    {

        public ContasController()
        {
        }

        /// <summary>
        /// Abertura de conta
        /// </summary>
        /// <param name="request"></param>
        /// <param name="abrirContaUseCase"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<AbrirContaResponse> CriarConta([FromBody] AbrirContaRequest request, [FromServices] IAbrirContaUseCase abrirContaUseCase)
        {
            var result = await abrirContaUseCase.Execute(request.NomeCliente, request.CpfCliente, request.NumeroAgencia);

            return new AbrirContaResponse()
            {
                ContaCorrente = new Models.ContaCorrente() {Id=result.Conta.Id, DigitoConta = result.Conta.DigitoConta, NumeroAgencia = result.Conta.NumeroAgencia, NumeroConta = result.Conta.NumeroConta },
                Cliente = new Cliente() { Cpf = result.Cliente.Cpf, Nome = result.Cliente.Nome }
            };

        }

        /// <summary>
        /// Obtém detalhes de uma conta
        /// </summary>
        /// <param name="contaId">Identificador da conta (Guid)</param>
        /// <param name="detalharContaUseCase"></param>
        /// <returns></returns>
        [HttpGet("{contaId}")]
        public async Task<DetalharContaResponse> DetalharConta([FromRoute] string contaId, [FromServices]IDetalharContaUseCase detalharContaUseCase)
        {
            var result = await detalharContaUseCase.Execute(Guid.Parse(contaId));

            return new DetalharContaResponse()
            {
                ContaCorrente = new Models.ContaCorrente()
                {
                    Id = result.ContaCorrente.Id,
                    DigitoConta = result.ContaCorrente.DigitoConta,
                    NumeroAgencia = result.ContaCorrente.NumeroAgencia,
                    NumeroConta = result.ContaCorrente.NumeroConta
                },
                Saldo = result.SaldoAtual
            };
        }

        #region Crédito

        /// <summary>
        /// Lança um crédito em uma conta
        /// </summary>
        /// <param name="contaId">Identificador da conta (Guid)</param>
        /// <param name="request"></param>
        /// <param name="creditarUseCase"></param>
        /// <returns></returns>
        [HttpPost("{contaId}/creditos")]
        public async Task<CreditarResponse> Creditar([FromRoute] string contaId, [FromBody] CreditarRequest request, [FromServices] ICreditarUseCase creditarUseCase)
        {
            var result = await creditarUseCase.Execute(Guid.Parse(contaId), request.Valor);

            return new CreditarResponse() { SaldoAtualizado = result.SaldoAtual };
        }

        #endregion

        #region Débito

        /// <summary>
        /// Lança um débito em uma conta
        /// </summary>
        /// <param name="contaId">Identificador da conta (Guid)</param>
        /// <param name="request"></param>
        /// <param name="abrirContaUseCase"></param>
        /// <returns></returns>
        [HttpPost("{contaId}/debitos")]
        public async Task<DebitarResponse> Debitar([FromRoute]string contaId, [FromBody] DebitarRequest request, [FromServices] IDebitarUseCase abrirContaUseCase)
        {
            var result = await abrirContaUseCase.Execute(Guid.Parse(contaId), request.Valor);

            return new DebitarResponse() { SaldoAtualizado = result.SaldoAtual };
        }

        #endregion

        #region Transferência
        /// <summary>
        /// Realiza transferência entre contas correntes
        /// </summary>
        /// <param name="transferirRequest"></param>
        /// <param name="transferirUseCase"></param>
        /// <returns></returns>
        [HttpPost("transferir")]
        public async Task<TransferirResponse> Transferir([FromBody]TransferirRequest transferirRequest, [FromServices] ITransferirUseCase transferirUseCase)
        {
            var result = await transferirUseCase.Execute(transferirRequest.ContaOrigem.NumeroAgencia,
                transferirRequest.ContaOrigem.NumeroConta,
                transferirRequest.ContaOrigem.DigitoConta,
                transferirRequest.ContaDestino.NumeroAgencia,
                transferirRequest.ContaDestino.NumeroConta,
                transferirRequest.ContaDestino.DigitoConta,
                transferirRequest.Valor);

            return new TransferirResponse()
            {
                DataTransacao = result.DataTransacao,
                TransacaoId = result.TransacaoId
            };
        }

        #endregion
    }
}