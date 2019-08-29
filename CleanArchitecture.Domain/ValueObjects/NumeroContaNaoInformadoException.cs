using System;
using System.Runtime.Serialization;

namespace CleanArchitecture.Domain.ValueObjects
{
    [Serializable]
    public class NumeroContaNaoInformadoException : DomainException
    {
        public NumeroContaNaoInformadoException():base("Número da conta é obrigatório.")
        {
        }
    }
}