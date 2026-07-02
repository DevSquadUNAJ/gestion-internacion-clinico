using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.ICasosDeUso
{
    public interface IObtenerDosisProgramadasCasoDeUso
    {
        Task<PaginaRespuesta<DosisProgramadaRespuesta>> EjecutarAsync(
            Guid enfermeraId,
            FiltroDosisProgramadasSolicitud filtro,
            CancellationToken cancellationToken);
    }
}