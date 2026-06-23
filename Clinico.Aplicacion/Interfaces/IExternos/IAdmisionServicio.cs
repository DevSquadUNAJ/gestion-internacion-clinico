using Clinico.Aplicacion.DTOs.Respuestas.Admision;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IExternos
{
    public interface IAdmisionServicio
    {
        Task<ContextoInternacionRespuesta> ObtenerContextoInternacionAsync(Guid internacionId);

        Task<IEnumerable<DetalleCamaRespuesta>> ObtenerCamasPorSectorAsync(Guid sectorId);
    }
}