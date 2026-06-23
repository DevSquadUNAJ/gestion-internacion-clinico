using Clinico.Aplicacion.DTOs.Respuestas;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.ICasosDeUso
{
    public interface IObtenerEnfermeraPanelDeControlCasoDeUso
    {
        Task<IEnumerable<EnfermeraPanelDeControlRespuesta>> EjecutarAsync(Guid enfermeraId, CancellationToken cancellationToken);
    }
}