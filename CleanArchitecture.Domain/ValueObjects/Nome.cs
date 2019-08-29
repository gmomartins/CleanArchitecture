using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.ValueObjects
{
    public class Nome
    {
        private string nome;

        public Nome(string nome)
        {
            if (String.IsNullOrWhiteSpace(nome))
                throw new NomeNaoInformadoException();

            this.nome = nome;
        }


        public override string ToString()
        {
            return nome.ToString();
        }

        public static implicit operator Nome(string nome)
        {
            return new Nome(nome);
        }

        public static implicit operator string(Nome nome)
        {
            return nome.nome;
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
                return obj.ToString() == nome;
            }

            return ((Nome)obj).nome == nome;
        }

        public override int GetHashCode()
        {
            return nome.GetHashCode();
        }
    }
}
