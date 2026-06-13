using Clinico.Aplicacion.DTOs.Respuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.ICasosDeUso
{
    public interface IGetNursingDashboardUseCase
    {
        Task<IReadOnlyCollection<NursingDashboardItemResponse>>
            ExecuteAsync(Guid nurseId,
            CancellationToken cancellationToken);
    }
}
