using Clinico.Dominio.Base;
using System;

namespace Clinico.Dominio.Entidades
{
    public class Auditoria : EntidadBase
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
