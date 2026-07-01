using System.Collections.Generic;

namespace Clinico.Dominio.Entidades
{
    public class CatalogoCie10
    {
        public string Codigo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string Categoria { get; set; } = string.Empty;

        public ICollection<Diagnostico> Diagnosticos { get; set; } = new List<Diagnostico>();
    }
}
