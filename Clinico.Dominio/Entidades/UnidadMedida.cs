using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Dominio.Entidades
{
    public class UnidadMedida : EntityBase
    {
        public string Nombre { get; set; } = string.Empty;

        public string Abreviatura { get; set; } = string.Empty;

        public ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
    }
}
