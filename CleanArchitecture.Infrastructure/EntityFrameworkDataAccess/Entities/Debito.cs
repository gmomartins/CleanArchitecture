using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infrastructure.EntityFrameworkDataAccess.Entities
{
    public class Debito
    {
                public Guid Id { get; set; }
        public Guid ContaId { get; set; }
        public double Valor { get; set; }
        public DateTime DataTransacao { get; set; }
        public Guid TransacaoId { get; set; }
    }
}
