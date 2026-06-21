using Clinico.Aplicacion.Interfaces.ISeguridad;
using Microsoft.AspNetCore.Http;

namespace Clinico.API.Servicios
{
    public class TokenUsuarioActual : ITokenUsuarioActual
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenUsuarioActual(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string? ObtenerToken()
        {
            var authorizationHeader = _contextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                return authorizationHeader.Substring("Bearer ".Length).Trim();
            }

            return null;
        }
    }
}