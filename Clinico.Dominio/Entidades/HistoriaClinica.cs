using Clinico.Dominio.Base;
using System;
using System.Collections.Generic;

namespace Clinico.Dominio.Entidades
{
    public class HistoriaClinica : EntidadBase
    {
        public Guid PacienteId { get; set; }

        public string GrupoSanguineo { get; set; } = string.Empty;

        public string? Alergias { get; set; }

        public string? Antecedentes { get; set; }

        public string? ObservacionesGenerales { get; set; }

        public ICollection<Diagnostico> Diagnosticos { get; set; } = new List<Diagnostico>();

        public ICollection<EvolucionClinica> EvolucionesClinicas { get; set; } = new List<EvolucionClinica>();
    }
}
