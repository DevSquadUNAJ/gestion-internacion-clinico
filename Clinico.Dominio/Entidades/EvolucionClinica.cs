using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Dominio.Entidades
{
    public class EvolucionClinica : EntityBase
    {
        public Guid HistoriaClinicaId { get; set; }

        public Guid MedicoId { get; set; }

        public DateTime FechaHora { get; set; }

        public string Observacion { get; set; } = string.Empty;

        public HistoriaClinica HistoriaClinica { get; set; } = null!;

        public Medico Medico { get; set; } = null!;
    }
}
