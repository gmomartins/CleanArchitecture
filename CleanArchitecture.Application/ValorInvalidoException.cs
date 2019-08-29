using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application
{
    public class ValorInvalidoException:ApplicationException
    {
        public ValorInvalidoException():base("Valor deve ser maior que zero.")
        {

        }
    }
}
