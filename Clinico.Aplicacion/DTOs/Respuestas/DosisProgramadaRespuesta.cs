using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.DTOs.Respuestas
{
    public sealed record DosisProgramadaRespuesta
    {
        public Guid DosisId { get; set; }
        public string Paciente { get; set; } = string.Empty;
        public string Medicamento { get; set; } = string.Empty;
        public DateTime FechaProgramada { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Prioridad { get; set; } = string.Empty;
    }
}
