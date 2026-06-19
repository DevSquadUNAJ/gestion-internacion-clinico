using System;
using System.Threading.Tasks;
using Clinico.Aplicacion.DTOs.Respuestas.Admision;
using Clinico.Aplicacion.Interfaces.IExternos;
using Clinico.Infraestructura.Refit;

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
            // Aquí en un futuro se puede agregar un bloque try-catch
            // para atrapar Refit.ApiException y lanzar excepciones propias de Clínico si Admisión se cae.
            // Por ahora, lo mantenemos simple.
            return await _admisionApi.ObtenerContextoInternacionAsync(internacionId);
        }
    }
}