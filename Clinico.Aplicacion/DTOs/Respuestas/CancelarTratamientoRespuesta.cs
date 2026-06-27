using Clinico.Dominio.Constantes;
using System;

namespace Clinico.Aplicacion.DTOs.Respuestas
{
    public class CancelarTratamientoRespuesta
    {
        public Guid TratamientoId { get; set; }
        public EstadoTratamiento Estado { get; set; }
        public DateTime FechaCancelacion { get; set; }
    }
}