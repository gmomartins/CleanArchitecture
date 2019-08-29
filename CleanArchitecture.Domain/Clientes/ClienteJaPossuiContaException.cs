using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Clientes
{
    public class ClienteJaPossuiContaException: DomainException
    {
        public ClienteJaPossuiContaException(string nomeCliente):base($"Cliente '{nomeCliente}' já possuí conta.")
        {

        }
    }
}
