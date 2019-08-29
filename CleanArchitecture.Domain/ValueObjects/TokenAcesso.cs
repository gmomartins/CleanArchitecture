using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.ValueObjects
{
    public class TokenAcesso
    {
        private string tokenAcesso;

        public TokenAcesso(string tokenAcesso)
        {
            if (String.IsNullOrWhiteSpace(tokenAcesso))
                throw new TokenAcessoNaoInformadoException();

            this.tokenAcesso = tokenAcesso;
        }


        public override string ToString()
        {
            return tokenAcesso.ToString();
        }

        public static implicit operator TokenAcesso(string tokenAcesso)
        {
            return new TokenAcesso(tokenAcesso);
        }

        public static implicit operator string(TokenAcesso tokenAcesso)
        {
            return tokenAcesso.tokenAcesso;
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
                return obj.ToString() == tokenAcesso;
            }

            return ((TokenAcesso)obj).tokenAcesso == tokenAcesso;
        }

        public override int GetHashCode()
        {
            return tokenAcesso.GetHashCode();
        }
    }
}
