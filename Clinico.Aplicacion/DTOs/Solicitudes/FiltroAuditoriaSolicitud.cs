using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.DTOs.Solicitudes
{
    public class FiltroAuditoriaSolicitud
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Entidad { get; set; } = string.Empty;
        public Guid EntidadId { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }
}
