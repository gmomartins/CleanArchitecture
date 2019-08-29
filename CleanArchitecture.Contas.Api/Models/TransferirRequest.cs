using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Contas.Api.Models
{
    public class TransferirRequest
    {
        public ContaCorrenteSimplificado ContaOrigem{ get; set; }
        public ContaCorrenteSimplificado ContaDestino { get; set; }
        public double Valor { get; set; }
    }
}
