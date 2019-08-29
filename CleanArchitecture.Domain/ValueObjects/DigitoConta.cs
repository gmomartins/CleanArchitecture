using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.ValueObjects
{
    public sealed class DigitoConta
    {
        private string digitoConta;

        public DigitoConta(string digitoConta)
        {
            if (string.IsNullOrWhiteSpace(digitoConta))
                throw new DigitoContaNaoInformadoException();

            this.digitoConta = digitoConta;
        }

        public override string ToString()
        {
            return digitoConta.ToString();
        }

        public static implicit operator DigitoConta(string digitoConta)
        {
            return new DigitoConta(digitoConta);
        }

        public static implicit operator string(DigitoConta digitoConta)
        {
            return digitoConta.digitoConta;
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
                return obj.ToString() == digitoConta;
            }

            return ((DigitoConta)obj).digitoConta == digitoConta;
        }

        public override int GetHashCode()
        {
            return digitoConta.GetHashCode();
        }
    }
}
