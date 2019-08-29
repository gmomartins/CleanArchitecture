using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Contas;

namespace CleanArchitecture.Application.Comandos.DetalharConta
{
    public interface IDetalharContaUseCase
    {
        Task<DetalharContaResult> Execute(Guid contaId);
    }
}
