using Clinico.Aplicacion.DTOs.Respuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.ICasosDeUso
{
    public interface IObtenerEnfermeraPanelDeControlCasoDeUso
    {
        Task<IReadOnlyCollection<EnfermeraPanelDeControlRespuesta>>
            EjecutarAsync(Guid nurseId,
            CancellationToken cancellationToken);
    }
}
