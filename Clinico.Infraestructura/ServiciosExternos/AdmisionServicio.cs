using System;
using System.Net;
using System.Threading.Tasks;
using Clinico.Aplicacion.DTOs.Respuestas.Admision;
using Clinico.Aplicacion.Interfaces.IExternos;
using Clinico.Aplicacion.Excepciones;
using Clinico.Infraestructura.Refit;
using Refit;

namespace Clinico.Infraestructura.ServiciosExternos
{
    public class AdmisionServicio : IAdmisionServicio
    {
        private readonly IAdmisionApi _admisionApi;

        public AdmisionServicio(IAdmisionApi admisionApi)
        {
            _admisionApi = admisionApi;
        }

        public async Task<ContextoInternacionRespuesta> ObtenerContextoInternacionAsync(Guid internacionId)
        {
            try
            {
                return await _admisionApi.ObtenerContextoInternacionAsync(internacionId);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ExceptionNotFound("La internación indicada no fue encontrada en el microservicio de Admisión.");
                }
                else if (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new ExceptionBadRequest("La internación indicada no se encuentra activa para registrar acciones médicas.");
                }
                else if (ex.StatusCode == HttpStatusCode.Unauthorized || ex.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new ExceptionUnauthorized("El usuario actual no tiene permisos o su sesión es inválida para comunicarse con Admisión.");
                }

                throw new Exception($"Error de infraestructura al comunicarse con el microservicio de Admisión: {ex.Message}", ex);
            }
        }
    }
}