using Clinico.Aplicacion.DTOs.Respuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IConsultas
{
    public interface IObtenerDosisProgramadasConsulta
    {
        Task<PaginaRespuesta<DosisProgramadaRespuesta>>
            ObtenerAsync(
                Guid sectorId,
                DateTime fecha,
                int pagina,
                int tamPagina,
                CancellationToken cancellationToken);
    }
}
