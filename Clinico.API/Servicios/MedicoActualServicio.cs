using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ISeguridad;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Clinico.API.Servicios
{
    public class MedicoActualServicio : IMedicoActualServicio
    {
        private const string ClaimEntidadAsociadaId = "EntidadAsociadaId";

        private readonly IHttpContextAccessor _contextAccessor;

        public MedicoActualServicio(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid ObtenerMedicoId()
        {
            var usuario = _contextAccessor.HttpContext?.User
                ?? throw new ExceptionUnauthorized("No hay contexto de usuario autenticado.");

            var claim = usuario.Claims.FirstOrDefault(c => c.Type == ClaimEntidadAsociadaId)
                ?? throw new ExceptionUnauthorized(
                    "El token no contiene el claim 'EntidadAsociadaId'. " +
                    "El usuario autenticado no está asociado a un médico.");

            if (!Guid.TryParse(claim.Value, out var medicoId))
                throw new ExceptionUnauthorized("El claim 'EntidadAsociadaId' no es un Guid válido.");

            return medicoId;
        }
    }
}