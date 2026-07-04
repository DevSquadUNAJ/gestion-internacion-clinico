using Clinico.Aplicacion.DTOs.Respuestas.Admision;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.IExternos;
using Clinico.Infraestructura.Refit;
using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<DetalleCamaRespuesta>> ObtenerCamasPorSectorAsync(Guid sectorId)
        {
            try
            {
                return await _admisionApi.ObtenerCamasPorSectorAsync(sectorId);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    throw new ExceptionNotFound("El sector indicado no existe en Admisión.");

                throw new Exception($"Error de infraestructura al comunicarse con el microservicio de Admisión: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<SectorOcupacionRespuesta>> ObtenerSectoresAsync()
        {
            try
            {
                return await _admisionApi.ObtenerSectoresAsync();
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized
                    || ex.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    throw new ExceptionUnauthorized(
                        "El usuario actual no tiene permisos o su sesión es inválida para comunicarse con Admisión.");
                }

                throw new Exception(
                    $"Error de infraestructura al comunicarse con el microservicio de Admisión: {ex.Message}", ex);
            }
        }
    }
}