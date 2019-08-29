using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.ValueObjects
{
    public sealed class Senha
    {
        private string senha;

        public static Senha Default = new Senha("123456");

        public Senha(string senha)
        {
            if (String.IsNullOrWhiteSpace(senha))
                throw new SenhaNaoInformadaException();

            this.senha = senha;
        }


        public override string ToString()
        {
            return senha;
        }

        public static implicit operator Senha(string senha)
        {
            return new Senha(senha);
        }

        public static implicit operator string(Senha senha)
        {
            return senha.senha;
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
                return obj.ToString() == senha;
            }

            return ((Senha)obj).senha == senha;
        }

        public override int GetHashCode()
        {
            return senha.GetHashCode();
        }
    }
}
