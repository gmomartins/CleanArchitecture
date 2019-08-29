using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.ValueObjects
{
    public sealed class NumeroConta
    {
        private string numeroConta;

        public NumeroConta(string numeroConta)
        {
            if (string.IsNullOrWhiteSpace(numeroConta))
                throw new NumeroContaNaoInformadoException();

            this.numeroConta = numeroConta;
        }

        public override string ToString()
        {
            return numeroConta.ToString();
        }

        public static implicit operator NumeroConta(string numeroConta)
        {
            return new NumeroConta(numeroConta);
        }

        public static implicit operator string(NumeroConta numeroConta)
        {
            return numeroConta.numeroConta;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is string)
            {
                return obj.ToString() == numeroConta;
            }

            return ((NumeroConta)obj).numeroConta == numeroConta;
        }

        public override int GetHashCode()
        {
            return numeroConta.GetHashCode();
        }
    }
}
