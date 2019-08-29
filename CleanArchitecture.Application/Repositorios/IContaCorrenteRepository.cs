using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Contas;

namespace CleanArchitecture.Application.Repositorios
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> Obter(Guid contaId);
        Task<ContaCorrente> Obter(string numeroAgencia, string numeroConta, string digitoConta);
        Task Salvar(ContaCorrente conta);
        Task SalvarTransacao(ContaCorrente conta, Debito debito);
        Task SalvarTransacao(ContaCorrente conta, Credito credito);
        Task<IList<Credito>> ObterCreditos(Guid contaId);
        Task<IList<Debito>> ObterDebitos(Guid contaId);
    }
}
