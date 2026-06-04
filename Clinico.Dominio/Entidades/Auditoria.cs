using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Dominio.Entidades
{
    public class Auditoria : EntityBase
    {
        public string UsuarioId { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;

        public string Accion { get; set; } = string.Empty;

        public string Entidad { get; set; } = string.Empty;

        public Guid EntidadId { get; set; }

        public string? Descripcion { get; set; }

        public string? PayloadJson { get; set; }

        public DateTime FechaHora { get; set; }
    }
}
