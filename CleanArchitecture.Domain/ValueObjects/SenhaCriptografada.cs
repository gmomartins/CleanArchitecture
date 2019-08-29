using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.ValueObjects
{
    public sealed class SenhaCriptografada
    {
        private string senha;

        private SenhaCriptografada(string senhaJaCriptografada)
        {
            this.senha = senhaJaCriptografada;
        }

        public SenhaCriptografada(Senha senha)
        {
            this.senha = Criptografar(senha);
        }

        private string Criptografar(Senha senha)
        {
            using (var sha512 = SHA512.Create())
            {
                var bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(senha));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public override string ToString()
        {
            return senha;
        }

        public static implicit operator SenhaCriptografada(Senha senha)
        {
            return new SenhaCriptografada(senha);
        }

        public static implicit operator SenhaCriptografada(string senha)
        {
            return new SenhaCriptografada(senha);
        }

        public static implicit operator string(SenhaCriptografada senha)
        {
            return senha?.senha;
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

            return ((SenhaCriptografada)obj).senha == senha;
        }

        public override int GetHashCode()
        {
            return senha.GetHashCode();
        }
    }
}
