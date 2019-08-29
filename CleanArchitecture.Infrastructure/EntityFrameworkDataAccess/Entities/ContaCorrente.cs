using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infrastructure.EntityFrameworkDataAccess.Entities
{
    public class ContaCorrente
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public string NumeroAgencia { get; set; }
        public string NumeroConta { get; set; }
        public string DigitoConta { get; set; }
    }
}
