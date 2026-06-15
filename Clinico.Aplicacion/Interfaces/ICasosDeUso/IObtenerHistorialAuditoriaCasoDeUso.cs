using Clinico.Aplicacion.CasosDeUso;
using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.ICasosDeUso
{
    public interface IObtenerHistorialAuditoriaCasoDeUso
    {
        Task<ObtenerHistorialAuditoriaRespuesta> EjecutarAsync(FiltroAuditoriaSolicitud filtros);
    }
}
