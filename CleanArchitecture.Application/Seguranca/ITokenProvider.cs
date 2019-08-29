using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Seguranca
{
    public interface ITokenProvider
    {
        TokenAcesso GerarToken(Cpf cpfUsuario, Guid usuarioId);
    }
}
