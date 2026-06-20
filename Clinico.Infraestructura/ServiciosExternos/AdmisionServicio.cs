using System;
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
            catch (ApiException)
            {
                throw new ExceptionNotFound("La internación indicada no existe o no se encuentra activa en Admisión.");
            }
        }
    }
}