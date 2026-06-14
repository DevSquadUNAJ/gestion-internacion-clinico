using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.DTOs.Solicitudes
{
    public class FiltroAuditoriaSolicitud
    {
        public Guid PacienteId { get; set; }
        public Guid ProfesionalId { get; set; }
        public Guid InternacionId { get; set; }
        public Guid TratamientoId { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }
}
