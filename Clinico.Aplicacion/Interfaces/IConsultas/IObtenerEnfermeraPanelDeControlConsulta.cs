using Clinico.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IConsultas
{
    public interface IObtenerEnfermeraPanelDeControlConsulta
    {
        Task<IReadOnlyCollection<EnfermeraPanelDeControlDto>>
            ObtenerSectorPendienteAsync(
                Guid sectorId,
                CancellationToken cancellationToken);
    }
}