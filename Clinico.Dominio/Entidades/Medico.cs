using Clinico.Dominio.Base;
using System.Collections.Generic;

namespace Clinico.Dominio.Entidades
{
    public class Medico : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;

        public string Matricula { get; set; } = string.Empty;

        public string Especialidad { get; set; } = string.Empty;

        public ICollection<Diagnostico> Diagnosticos { get; set; } = new List<Diagnostico>();

        public ICollection<EvolucionClinica> EvolucionesClinicas { get; set; } = new List<EvolucionClinica>();
    }
}
