using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Dominio.Entidades
{
    public class Medicamento : EntityBase
    {
        public string NombreComercial { get; set; } = string.Empty;

        public string DrogaGenerica { get; set; } = string.Empty;

        public string Presentacion { get; set; } = string.Empty;

        public string? Contraindicaciones { get; set; }

        public string? EfectosAdversos { get; set; }

        public string ViaAdministracion { get; set; } = string.Empty;

        public bool RequiereControl { get; set; }

        public ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
    }
}
