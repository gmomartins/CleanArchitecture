using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Comandos.Autenticar
{
    public interface IAutenticarUseCase
    {
        Task<AutenticarResult> Execute(Cpf cpf, Senha senhaCriptografada);
    }
}
