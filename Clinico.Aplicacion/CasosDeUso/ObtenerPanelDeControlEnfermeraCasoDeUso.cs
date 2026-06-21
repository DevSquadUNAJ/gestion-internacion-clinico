using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Aplicacion.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso
{
    public sealed class ObtenerPanelDeControlEnfermeraCasoDeUso
        : IGetNursingDashboardUseCase
    {
        private readonly IObtenerEnfermeraConsulta _nurseQuery;
        private readonly IObtenerEnfermeraPanelDeControlConsulta _dashboardQuery;

        public ObtenerPanelDeControlEnfermeraCasoDeUso(
            IObtenerEnfermeraConsulta nurseQuery,
            IObtenerEnfermeraPanelDeControlConsulta dashboardQuery)
        {
            _nurseQuery = nurseQuery;
            _dashboardQuery = dashboardQuery;
        }

        public async Task<IReadOnlyCollection<EnfermeraPanelDeControlObjetoRespuesta>>
            ExecuteAsync(
                Guid nurseId,
                CancellationToken cancellationToken)
        {
            var nurse =
                await _nurseQuery.GetByIdAsync(
                    nurseId,
                    cancellationToken);

            if (nurse is null)
                throw new EnfermeraNotFoundException(nurseId);

            var pendingDoses =
                await _dashboardQuery.GetPendingBySectorAsync(
                    nurse.SectorId,
                    cancellationToken);

            return pendingDoses
                .Select(d => new EnfermeraPanelDeControlObjetoRespuesta(
                    d.TreatmentDoseId,
                    d.PatientName,
                    d.MedicationName,
                    d.ScheduledTime,
                    d.ScheduledTime < DateTime.UtcNow
                        ? "High"
                        : "Normal"))
                .OrderBy(x => x.Priority == "High" ? 0 : 1)
                .ThenBy(x => x.ScheduledTime)
                .ToList();
        }
    }
}
