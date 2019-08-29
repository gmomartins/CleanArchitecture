using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application
{
    public class UsuarioOuSenhaInvalidosException : ApplicationException
    {
        public UsuarioOuSenhaInvalidosException() : base("Usuário/Senha inválidos.")
        {

        }
    }
}
