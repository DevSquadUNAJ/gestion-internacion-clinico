using System;
using System.Threading.Tasks;
using Clinico.Aplicacion.DTOs.Respuestas.Admision;
using Refit;

namespace Clinico.Infraestructura.Refit
{
    // Refit leerá esto y creará el HttpClient automáticamente
    public interface IAdmisionApi
    {
        [Get("/api/internaciones/{internacionId}/contexto")]
        Task<ContextoInternacionRespuesta> ObtenerContextoInternacionAsync(Guid internacionId);
    }
}