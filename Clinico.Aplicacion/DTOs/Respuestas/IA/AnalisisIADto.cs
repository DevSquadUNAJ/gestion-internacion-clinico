using System.Collections.Generic;
using Clinico.Dominio.Constantes;

namespace Clinico.Aplicacion.DTOs.Respuestas.IA
{
    public class AnalisisIADto
    {
        public NivelRiesgoIA NivelRiesgo { get; set; }
        public bool AlertaDetectada { get; set; }
        public List<AlertaIADto> Alertas { get; set; } = new();
        public SugerenciaIADto? Sugerencia { get; set; }
        public string ResumenClinico { get; set; } = string.Empty;
    }

}