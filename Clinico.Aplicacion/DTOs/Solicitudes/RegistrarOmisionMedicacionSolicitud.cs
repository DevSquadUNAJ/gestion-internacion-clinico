using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.DTOs.Solicitudes
{
    public sealed record RegistrarOmisionMedicacionSolicitud(
        string Motivo,
        string? Observaciones
    );
}
