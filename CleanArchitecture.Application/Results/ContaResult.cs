using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Results
{
    public class ContaResult
    {
        public string NumeroAgencia { get; set; }
        public string NumeroConta { get; set; }
        public string DigitoConta { get; set; }

        public ContaResult(string numeroAgencia, string numeroConta, string digitoConta)
        {
            this.NumeroAgencia = numeroAgencia;
            this.NumeroConta = numeroConta;
            this.DigitoConta = digitoConta;
        }
    }
}

