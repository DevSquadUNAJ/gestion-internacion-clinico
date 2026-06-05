using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Dominio.Entidades
{
    public class FrecuenciaAdministracion : EntityBase
    {
        public string Descripcion { get; set; } = string.Empty;

        public int CantidadHoras { get; set; }

        public ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
    }
}
