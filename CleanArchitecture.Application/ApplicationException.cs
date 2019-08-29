using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application
{
    public abstract class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message)
        {
        }
    }
}
