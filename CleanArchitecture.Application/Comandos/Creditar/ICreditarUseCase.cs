using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Comandos.Creditar
{
    public interface ICreditarUseCase
    {
        Task<CreditarResult> Execute(Guid contaId, Valor valor);
    }
}
