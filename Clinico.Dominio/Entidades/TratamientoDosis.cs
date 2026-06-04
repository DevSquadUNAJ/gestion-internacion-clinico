using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Dominio.Entidades
{
    public class TratamientoDosis : EntityBase
    {
        public Guid TratamientoId { get; set; }

        public Guid EnfermeraId { get; set; }

        public DateTime FechaProgramada { get; set; }

        public DateTime? FechaSuministro { get; set; }

        public DateTime FechaDelSistema { get; set; }

        public EstadoDosis Estado { get; set; }

        public string? MotivoOmision { get; set; }

        public string? Observaciones { get; set; }

        public Tratamiento Tratamiento { get; set; } = null!;

        public Enfermera Enfermera { get; set; } = null!;
    }
}
