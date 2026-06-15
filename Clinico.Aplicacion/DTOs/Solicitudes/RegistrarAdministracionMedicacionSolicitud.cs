using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.DTOs.Solicitudes
{
    public sealed record RegistrarAdministracionMedicacionSolicitud(
        DateTime SuppliedAt,
        string? Observations
    );
}
