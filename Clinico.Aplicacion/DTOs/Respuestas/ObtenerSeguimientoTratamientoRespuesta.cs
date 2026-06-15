using Clinico.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.DTOs.Respuestas
{
    public class ObtenerSeguimientoTratamientoRespuesta
    {
        public Guid TratamientoId { get; set; }

        public List<DosisSeguimientoRespuesta> Dosis { get; set; } = [];
    }

    public class DosisSeguimientoRespuesta
    {
        public DateTime FechaProgramada { get; set; }

        public DateTime? FechaSuministro { get; set; }

        public EstadoDosis Estado { get; set; }

        public string? Observaciones { get; set; }
    }
}
