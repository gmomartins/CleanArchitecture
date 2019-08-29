using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infrastructure.EntityFrameworkDataAccess.Entities
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
    }
}
