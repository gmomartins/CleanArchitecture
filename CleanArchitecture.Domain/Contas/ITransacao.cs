using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Contas
{
    public interface ITransacao
    {
        Guid TransacaoId { get; }
        Guid ContaId { get; }
        Valor Valor { get; }
        DateTime DataTransacao { get; }
    }
}
