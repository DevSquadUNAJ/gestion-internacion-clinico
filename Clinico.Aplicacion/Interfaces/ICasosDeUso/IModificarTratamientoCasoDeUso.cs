using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using System;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.ICasosDeUso;

public interface IModificarTratamientoCasoDeUso
{
    Task<ModificarTratamientoRespuesta> EjecutarAsync(Guid tratamientoId, ModificarTratamientoSolicitud solicitud);
}
