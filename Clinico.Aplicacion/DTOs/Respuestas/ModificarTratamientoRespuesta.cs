using Clinico.Dominio.Constantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.DTOs.Respuestas
{
    public class ModificarTratamientoRespuesta
    {
        public Guid TratamientoId { get; set; }

        public decimal Dosis { get; set; }

        public string Frecuencia { get; set; } = string.Empty;

        public EstadoTratamiento Estado { get; set; }

        public int DosisCanceladas { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
