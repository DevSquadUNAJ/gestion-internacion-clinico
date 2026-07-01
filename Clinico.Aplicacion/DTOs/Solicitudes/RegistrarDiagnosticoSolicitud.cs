using System;

namespace Clinico.Aplicacion.DTOs.Solicitudes
{
    public class RegistrarDiagnosticoSolicitud
    {
        public Guid InternacionId { get; set; }
        public Guid MedicoId { get; set; }
        public string CodigoCie10 { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
    }
}