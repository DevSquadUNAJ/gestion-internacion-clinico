using System;

namespace Clinico.Aplicacion.DTOs.Respuestas.Admision
{
    public class ContextoInternacionRespuesta
    {
        public Guid PacienteId { get; set; }
        public string NombrePaciente { get; set; } = string.Empty;
        public Guid CamaId { get; set; }
        public int NumeroCama { get; set; }
        public Guid SectorId { get; set; }
        public string NombreSector { get; set; } = string.Empty;
    }
}