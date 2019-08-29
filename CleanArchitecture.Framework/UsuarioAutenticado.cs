using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CleanArchitecture.Domain.Seguranca;

namespace CleanArchitecture.Framework
{
    public sealed class UsuarioAutenticado : IUsuarioAutenticado
    {
        private readonly IHttpContextAccessor _accessor;

        public UsuarioAutenticado(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Cpf
        {
            get
            {
                return ObterClaimValue("cpf");
            }
        }

        public Guid Id
        {
            get
            {
                string id = ObterClaimValue("id");

                if (!String.IsNullOrWhiteSpace(id))
                    return Guid.Parse(id);

                return default(Guid);
            }
        }

        private IEnumerable<Claim> ObterClaims()
        {
            if (_accessor?.HttpContext?.User != null)
                return _accessor.HttpContext.User.Claims;
            return null;
        }

        private string ObterClaimValue(string claimType)
        {
            if (_accessor?.HttpContext?.User != null)
                return _accessor.HttpContext.User.Claims.FirstOrDefault(s => s.Type.Equals(claimType))?.Value;
            return null;
        }
    }
}
