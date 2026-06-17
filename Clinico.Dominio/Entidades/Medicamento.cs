using Clinico.Dominio.Base;
using Clinico.Dominio.Constantes;
using System.Collections.Generic;

namespace Clinico.Dominio.Entidades
{
    public class Medicamento : EntidadBase
    {
        public string NombreComercial { get; set; } = string.Empty;

        public string DrogaGenerica { get; set; } = string.Empty;

        public string Presentacion { get; set; } = string.Empty;

        public string? Contraindicaciones { get; set; }

        public string? EfectosAdversos { get; set; }

        public ViaAdministracion ViaAdministracion { get; set; }

        public bool RequiereControl { get; set; }

        public ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
    }
}