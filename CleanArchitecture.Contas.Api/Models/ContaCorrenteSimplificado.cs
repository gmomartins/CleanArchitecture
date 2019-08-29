using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Contas.Api.Models
{
    public class ContaCorrenteSimplificado
    {
        public string NumeroAgencia { get; set; }
        public string NumeroConta { get; set; }
        public string DigitoConta { get; set; }
    }
}
