using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Dominio.Entidades
{
    public class AuditoriaIA : EntityBase
    {
        public Guid TratamientoId { get; set; }

        public bool AlertaDetectada { get; set; }

        public string MensajeIA { get; set; } = string.Empty;

        public bool FueForzado { get; set; }

        public string? JustificacionClinica { get; set; }

        public DateTime FechaHora { get; set; }

        public Tratamiento Tratamiento { get; set; } = null!;
    }
}
