using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infrastructure
{
    public class ClienteNaoEncontradoException:InfrastructureException
    {
        public ClienteNaoEncontradoException(Guid clienteId):base($"Cliente '{clienteId}' não encontrado.")
        {

        }

        public ClienteNaoEncontradoException(string cpf):base($"Nenhum cliente com cpf '{cpf}' encontrado.")
        {

        }
    }
}
