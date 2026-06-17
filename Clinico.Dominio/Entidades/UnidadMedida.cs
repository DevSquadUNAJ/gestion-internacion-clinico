using Clinico.Dominio.Base;
using System.Collections.Generic;

namespace Clinico.Dominio.Entidades
{
    public class UnidadMedida : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;

        public string Abreviatura { get; set; } = string.Empty;

        public ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
    }
}
