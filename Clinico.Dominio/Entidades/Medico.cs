using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Dominio.Entidades
{
    public class Medico : EntityBase
    {
        public string Nombre { get; set; } = string.Empty;

        public string Matricula { get; set; } = string.Empty;

        public string Especialidad { get; set; } = string.Empty;

        public ICollection<Diagnostico> Diagnosticos { get; set; } = new List<Diagnostico>();

        public ICollection<EvolucionClinica> EvolucionesClinicas { get; set; } = new List<EvolucionClinica>();
    }
}
