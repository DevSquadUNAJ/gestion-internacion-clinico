using Clinico.Aplicacion.DTOs.Respuestas;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.ICasosDeUso
{
    public interface IObtenerSectorEnfermeraCasoDeUso
    {
        Task<SectorEnfermeraRespuesta> EjecutarAsync(
            Guid enfermeraId,
            CancellationToken cancellationToken);
    }
}