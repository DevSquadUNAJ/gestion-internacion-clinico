using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.DTOs.Respuestas
{
    public sealed record NursingDashboardItemResponse
    (
        Guid TreatmentDoseId,
        string Patient,
        string Medication,
        DateTime ScheduledTime,
        string Priority
    );
}
