using Clinico.Dominio.Base;
using System;

namespace Clinico.Dominio.Entidades
{
    public class EvolucionClinica : EntidadBase
    {
        public Guid HistoriaClinicaId { get; set; }

        public Guid MedicoId { get; set; }

        public DateTime FechaHora { get; set; }

        public string Observacion { get; set; } = string.Empty;

        public HistoriaClinica HistoriaClinica { get; set; } = null!;
    }
}
