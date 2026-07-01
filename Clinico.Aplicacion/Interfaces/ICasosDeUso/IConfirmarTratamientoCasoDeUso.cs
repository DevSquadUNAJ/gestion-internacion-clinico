using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.ICasosDeUso
{
    public interface IConfirmarTratamientoCasoDeUso
    {
        Task<ConfirmarTratamientoRespuesta> EjecutarAsync(
            Guid tratamientoId,
            Guid medicoId,
            ConfirmarTratamientoSolicitud solicitud,
            CancellationToken cancellationToken);
    }
}