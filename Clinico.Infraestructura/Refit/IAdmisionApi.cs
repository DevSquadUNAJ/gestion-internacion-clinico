using Clinico.Aplicacion.DTOs.Respuestas.Admision;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Refit
{
    // Refit leerá esto y creará el HttpClient automáticamente
    public interface IAdmisionApi
    {
        [Get("/api/internaciones/{internacionId}/contexto")]
        Task<ContextoInternacionRespuesta> ObtenerContextoInternacionAsync(Guid internacionId);

        [Get("/api/sectores/{sectorId}/camas")]
        Task<IEnumerable<DetalleCamaRespuesta>> ObtenerCamasPorSectorAsync(Guid sectorId);

        [Get("/api/sectores")]
        Task<IEnumerable<SectorOcupacionRespuesta>> ObtenerSectoresAsync();
    }
}