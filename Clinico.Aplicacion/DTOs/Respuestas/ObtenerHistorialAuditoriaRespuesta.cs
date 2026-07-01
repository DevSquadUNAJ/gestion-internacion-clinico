using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.DTOs.Respuestas
{
    public class ObtenerHistorialAuditoriaRespuesta
    {
        public List<AuditoriaRespuesta> Auditorias { get; set; } = new();
    }
    public class AuditoriaRespuesta
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public string Entidad { get; set; } = string.Empty;
        public Guid EntidadId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string PayloadJson { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
    }

}
