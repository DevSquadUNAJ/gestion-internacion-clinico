using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinico.Aplicacion.DTOs;

namespace Clinico.Aplicacion.Interfaces.IConsultas
{
    public interface ITratamientoDosisRepositorio
    {
        Task<IReadOnlyCollection<EnfermeraPanelDeControlDto>>
            ObtenerSectorPendienteAsync(
                Guid sectorId,
                CancellationToken cancellationToken);
    }
}
