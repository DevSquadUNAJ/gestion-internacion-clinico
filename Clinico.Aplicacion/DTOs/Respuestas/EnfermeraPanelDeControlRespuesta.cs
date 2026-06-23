using System;

namespace Clinico.Aplicacion.DTOs.Respuestas
{
    public class EnfermeraPanelDeControlRespuesta
    {
        public Guid DosisId { get; set; }
        public int NumeroCama { get; set; }
        public string Paciente { get; set; } = string.Empty;
        public string Medicamento { get; set; } = string.Empty;
        public DateTime FechaProgramada { get; set; }
        public string Prioridad { get; set; } = string.Empty;
    }
}