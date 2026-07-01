using Clinico.Aplicacion.DTOs.Respuestas.IA;
using Clinico.Dominio.Constantes;
using System;

namespace Clinico.Aplicacion.DTOs.Respuestas
{
    public class PrescribirTratamientoRespuesta
    {
        public Guid TratamientoId { get; set; }
        public EstadoTratamiento Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

        public AnalisisIADto? AnalisisIA { get; set; }
        public EstadoAnalisisIA EstadoAnalisis { get; set; }
    }

}