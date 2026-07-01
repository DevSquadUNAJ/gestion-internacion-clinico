using Clinico.Dominio.Base;
using System.Collections.Generic;

namespace Clinico.Dominio.Entidades
{
    public class FrecuenciaAdministracion : EntidadBase
    {
        public string Descripcion { get; set; } = string.Empty;

        public int CantidadHoras { get; set; }

        public ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
    }
}
