using System;

namespace Clinico.Aplicacion.DTOs.Respuestas.Admision
{
    public class DetalleCamaRespuesta
    {
        public Guid CamaId { get; set; }
        public int Numero { get; set; }
        public int Estado { get; set; }
        public Guid? PacienteId { get; set; }
        public string? PacienteAsignado { get; set; }
    }
}