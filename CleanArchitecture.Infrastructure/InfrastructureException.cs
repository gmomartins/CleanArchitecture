using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infrastructure
{
    public abstract class InfrastructureException : Exception
    {
        public InfrastructureException(string message) : base(message)
        {
        }
    }
}
