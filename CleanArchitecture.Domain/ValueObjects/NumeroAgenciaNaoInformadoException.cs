using System;
using System.Runtime.Serialization;

namespace CleanArchitecture.Domain.ValueObjects
{
    [Serializable]
    public class NumeroAgenciaNaoInformadoException : DomainException
    {
        public NumeroAgenciaNaoInformadoException():base("Número da agência é obrigatório.")
        {
        }
    }
}