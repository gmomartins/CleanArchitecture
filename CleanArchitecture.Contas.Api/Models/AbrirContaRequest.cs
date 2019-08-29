using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Contas.Api.Models
{
    public class AbrirContaRequest
    {
        public string NomeCliente { get; set; }
        public string CpfCliente { get; set; }
        public string NumeroAgencia { get; set; }
    }
}
