using System;

namespace Clinico.Aplicacion.DTOs.Solicitudes
{
    public class RegistrarAdministracionMedicacionSolicitud
    {
        public DateTime FechaSuministro { get; set; }
        public string? Observaciones { get; set; }
    }
}