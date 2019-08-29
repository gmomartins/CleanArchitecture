using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.ValueObjects
{
    public sealed class Cpf
    {
        private string cpf;

        public Cpf(string cpf)
        {
            if (!IsValid(cpf))
                throw new CpfInvalidoException(cpf);

            this.cpf = cpf;
        }

        public static bool IsValid(string cpf)
        {
            return !String.IsNullOrWhiteSpace(cpf);
        }

        public override string ToString()
        {
            return cpf.ToString();
        }

        public static implicit operator Cpf(string cpf)
        {
            return new Cpf(cpf);
        }

        public static implicit operator string(Cpf cpf)
        {
            return cpf.cpf;
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
                return obj.ToString() == cpf;
            }

            return ((Cpf)obj).cpf == cpf;
        }

        public override int GetHashCode()
        {
            return cpf.GetHashCode();
        }
    }
}
