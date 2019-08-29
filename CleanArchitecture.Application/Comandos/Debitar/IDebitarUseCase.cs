using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Comandos.Debitar
{
    public interface IDebitarUseCase
    {
        Task<DebitarResult> Execute(Guid contaId, Valor valor);
    }
}
