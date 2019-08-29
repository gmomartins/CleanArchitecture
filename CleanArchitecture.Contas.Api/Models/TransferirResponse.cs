using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Contas.Api.Models
{
    public class TransferirResponse
    {
        public DateTime DataTransacao { get; set; }
        public Guid TransacaoId { get; set; }
    }
}
