using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Comandos.AbrirConta
{
    public interface IAbrirContaUseCase
    {
        Task<AbrirContaResult> Execute(Nome nomeCliente, Cpf cpfCliente, NumeroAgencia numeroAgencia);
    }
}
