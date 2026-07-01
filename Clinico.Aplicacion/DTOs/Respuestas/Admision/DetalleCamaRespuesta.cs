using System;

namespace Clinico.Aplicacion.DTOs.Respuestas.Admision
{
    public class DetalleCamaRespuesta
    {
        public Guid CamaId { get; set; }
        public int Numero { get; set; }
        public string Estado { get; set; } = string.Empty;
        public Guid? PacienteId { get; set; }
        public string? PacienteAsignado { get; set; }
    }
}