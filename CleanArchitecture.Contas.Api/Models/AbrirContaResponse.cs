﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Contas.Api.Models
{
    public class AbrirContaResponse
    {
        public ContaCorrente ContaCorrente { get; set; }
        public Cliente Cliente { get; set; }
    }
}
