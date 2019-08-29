using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Domain.Clientes;
using CleanArchitecture.Domain.Contas;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Comandos.AbrirConta
{
    public class AbrirContaUseCase : IAbrirContaUseCase
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IContaCorrenteRepository contaRepository;

        public AbrirContaUseCase(IClienteRepository clienteRepository, IContaCorrenteRepository contaRepository)
        {
            this.clienteRepository = clienteRepository;
            this.contaRepository = contaRepository;
        }

        public async Task<AbrirContaResult> Execute(Nome nomeCliente, Cpf cpfCliente, NumeroAgencia numeroAgencia)
        {
           
           Cliente cliente=await this.clienteRepository.ObterPorCpf(cpfCliente);

            if (cliente != null)
                throw new ClienteJaPossuiContaException(cliente.Nome);

            cliente = new Cliente(nomeCliente, cpfCliente);

            await this.clienteRepository.Salvar(cliente);

            NumeroConta numeroConta = DateTime.Now.Ticks.ToString();//geração de um novo número de conta
            DigitoConta digitoConta = (new Random()).Next(1, 9).ToString();//geração do digito verificador

            ContaCorrente conta = new ContaCorrente(cliente.Id, numeroAgencia, numeroConta, digitoConta);

            await this.contaRepository.Salvar(conta);

            return new AbrirContaResult()
            {
                Cliente = cliente,
                Conta = conta
            };
        }
    }
}
