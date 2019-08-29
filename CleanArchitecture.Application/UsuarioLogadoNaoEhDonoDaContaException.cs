using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application
{
    public class UsuarioLogadoNaoEhDonoDaContaException:ApplicationException
    {
        public UsuarioLogadoNaoEhDonoDaContaException(Guid contaId):base($"Usuário logado não é o dono da conta '{contaId}'.")
        {

        }
    }
}
