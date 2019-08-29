using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Seguranca
{
    public interface IUsuarioAutenticado
    {
        string Cpf { get; }
        Guid Id { get; }
    }
}
