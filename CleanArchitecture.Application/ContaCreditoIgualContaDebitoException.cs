using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application
{
    public class ContaCreditoIgualContaDebitoException : ApplicationException
    {
        public ContaCreditoIgualContaDebitoException():base($"Conta de destino não pode ser a conta de origem.")
        {

        }
    }
}
