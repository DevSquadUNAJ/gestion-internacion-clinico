using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Dominio.Entidades
{
    public class Diagnostico : EntityBase
    {
        public Guid HistoriaClinicaId { get; set; }

        public Guid MedicoId { get; set; }

        public string CodigoCie10 { get; set; } = string.Empty;

        public DateTime FechaHora { get; set; }

        public string? Observaciones { get; set; }

        public HistoriaClinica HistoriaClinica { get; set; } = null!;

        public Medico Medico { get; set; } = null!;

        public CatalogoCie10 CatalogoCie10 { get; set; } = null!;

        public ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
    }
}
