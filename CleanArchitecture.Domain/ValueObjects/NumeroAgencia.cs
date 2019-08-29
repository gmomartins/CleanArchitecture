using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.ValueObjects
{
    public sealed class NumeroAgencia
    {
        private string numeroAgencia;

        public NumeroAgencia(string numeroAgencia)
        {
            if (string.IsNullOrWhiteSpace(numeroAgencia))
                throw new NumeroAgenciaNaoInformadoException();

            this.numeroAgencia = numeroAgencia;
        }

        public override string ToString()
        {
            return numeroAgencia.ToString();
        }

        public static implicit operator NumeroAgencia(string text)
        {
            return new NumeroAgencia(text);
        }

        public static implicit operator string(NumeroAgencia numeroAgencia)
        {
            return numeroAgencia.numeroAgencia;
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
                return obj.ToString() == numeroAgencia;
            }

            return ((NumeroAgencia)obj).numeroAgencia == numeroAgencia;
        }

        public override int GetHashCode()
        {
            return numeroAgencia.GetHashCode();
        }
    }
}
