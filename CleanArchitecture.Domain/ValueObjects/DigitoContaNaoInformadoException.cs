using System;
using System.Runtime.Serialization;

namespace CleanArchitecture.Domain.ValueObjects
{
    [Serializable]
    public class DigitoContaNaoInformadoException : DomainException
    {
        public DigitoContaNaoInformadoException():base("Digito da conta é obrigatório.")
        {
        }
    }
}