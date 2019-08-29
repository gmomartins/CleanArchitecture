using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using CleanArchitecture.Application.Seguranca;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Framework
{
    public class JwtTokenProvider : ITokenProvider
    {
        private readonly TokenConfigurations tokenConfigurations;
        private readonly SigningConfigurations signingConfigurations;

        public JwtTokenProvider(TokenConfigurations tokenConfigurations, SigningConfigurations signingConfigurations)
        {
            this.tokenConfigurations = tokenConfigurations;
            this.signingConfigurations = signingConfigurations;
        }

        public TokenAcesso GerarToken(Cpf cpfUsuario, Guid usuarioId)
        {
            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(cpfUsuario, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim("cpf",cpfUsuario.ToString()),
                        new Claim("id",usuarioId.ToString())                    }
                );

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            return new TokenAcesso(token);
        }
    }
}
