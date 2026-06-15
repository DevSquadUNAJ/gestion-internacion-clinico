using Clinico.Aplicacion.CasosDeUso;
using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Clinico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialAuditoriaController : ControllerBase
    {
        private readonly IObtenerHistorialAuditoriaCasoDeUso _obtenerHistorialAuditoriaCasoDeUso;
        public HistorialAuditoriaController(IObtenerHistorialAuditoriaCasoDeUso obtenerHistorialAuditoriaCasoDeUso)
        {
            _obtenerHistorialAuditoriaCasoDeUso = obtenerHistorialAuditoriaCasoDeUso;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ObtenerHistorialAuditoriaRespuesta), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObtenerHistorialAuditoriaRespuesta>> ObtenerHistorial([FromQuery] FiltroAuditoriaSolicitud filtros)
        {
            var resultado = await _obtenerHistorialAuditoriaCasoDeUso.EjecutarAsync(filtros);
            return Ok(resultado);
        }
    }
}
