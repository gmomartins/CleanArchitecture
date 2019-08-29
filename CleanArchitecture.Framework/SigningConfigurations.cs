using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace CleanArchitecture.Framework
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations()
        {
            var secretKey = "7b140cc5-dbc2-4464-8bd8-c940c7bc9e82";

            Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            SigningCredentials = new SigningCredentials(
                Key,
                SecurityAlgorithms.HmacSha256);
        }
    }
}