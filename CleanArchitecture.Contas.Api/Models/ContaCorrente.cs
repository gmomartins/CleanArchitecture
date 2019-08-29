using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Contas.Api.Models
{
    public class ContaCorrente
    {
        public Guid? Id { get; set; }
        public string NumeroAgencia { get; set; }
        public string NumeroConta { get; set; }
        public string DigitoConta { get; set; }
    }
}
