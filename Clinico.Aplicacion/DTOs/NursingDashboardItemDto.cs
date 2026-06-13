using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.DTOs
{
    public sealed record NursingDashboardItemDto(
        Guid TreatmentDoseId,
        string PatientName,
        string MedicationName,
        DateTime ScheduledTime
    );
}
