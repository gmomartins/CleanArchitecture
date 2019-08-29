using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Framework
{
    public class JwtBearerOptionsHelper
    {

        public static void Configure(TokenValidationParameters paramsValidation, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            paramsValidation.ValidateAudience = true;
            paramsValidation.ValidAudience = tokenConfigurations.Audience;

            paramsValidation.ValidateIssuer = true;
            paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

            // Valida a assinatura de um token recebido
            paramsValidation.ValidateIssuerSigningKey = true;
            paramsValidation.IssuerSigningKey = signingConfigurations.Key;

            // Verifica se um token recebido ainda é válido
            paramsValidation.ValidateLifetime = true;

            // Tempo de tolerância para a expiração de um token (utilizado
            // caso haja problemas de sincronismo de horário entre diferentes
            // computadores envolvidos no processo de comunicação)
            paramsValidation.ClockSkew = TimeSpan.Zero;
        }
    }
}
