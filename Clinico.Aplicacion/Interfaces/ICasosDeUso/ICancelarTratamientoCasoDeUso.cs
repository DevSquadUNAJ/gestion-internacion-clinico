using Clinico.Aplicacion.DTOs.Respuestas;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.ICasosDeUso
{
    public interface ICancelarTratamientoCasoDeUso
    {
        Task<CancelarTratamientoRespuesta> EjecutarAsync(
            Guid tratamientoId,
            Guid medicoId,
            CancellationToken cancellationToken);
    }
}