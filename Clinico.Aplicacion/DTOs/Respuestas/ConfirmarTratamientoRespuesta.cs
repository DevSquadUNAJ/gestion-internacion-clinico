using Clinico.Dominio.Constantes;
using System;

namespace Clinico.Aplicacion.DTOs.Respuestas
{
    public class ConfirmarTratamientoRespuesta
    {
        public Guid TratamientoId { get; set; }
        public EstadoTratamiento Estado { get; set; }
        public int CantidadDosisGeneradas { get; set; }
        public DateTime? PrimeraDosis { get; set; }
        public DateTime? UltimaDosis { get; set; }
        public DateTime FechaConfirmacion { get; set; }
    }
}