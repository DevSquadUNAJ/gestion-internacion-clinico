using Clinico.Aplicacion.CasosDeUso;
using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Application.DTOs.Respuestas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.API.Controllers
{
    [ApiController]
    [Route("api/enfermeras")]
    [Authorize(Roles = "Enfermera")]
    public class EnfermerasController : ControllerBase
    {
        private readonly IObtenerEnfermeraPanelDeControlCasoDeUso _obtenerPanelCasoDeUso;
        private readonly IRegistrarAdministracionMedicacionCasoDeUso _registrarAdministracionCasoDeUso;
        private readonly IRegistrarOmisionMedicacionCasoDeUso _registrarOmisionCasoDeUso;
        private readonly IObtenerDosisProgramadasCasoDeUso _obtenerDosisProgramadasCasoDeUso;
        private readonly IObtenerSectorEnfermeraCasoDeUso _obtenerSectorEnfermeraCasoDeUso;

        public EnfermerasController(
            IObtenerEnfermeraPanelDeControlCasoDeUso obtenerPanelCasoDeUso,
            IRegistrarAdministracionMedicacionCasoDeUso registrarAdministracionCasoDeUso,
            IRegistrarOmisionMedicacionCasoDeUso registrarOmisionCasoDeUso,
            IObtenerDosisProgramadasCasoDeUso obtenerDosisProgramadasCasoDeUso,
            IObtenerSectorEnfermeraCasoDeUso obtenerSectorEnfermeraCasoDeUso)
        {
            _obtenerPanelCasoDeUso = obtenerPanelCasoDeUso;
            _registrarAdministracionCasoDeUso = registrarAdministracionCasoDeUso;
            _registrarOmisionCasoDeUso = registrarOmisionCasoDeUso;
            _obtenerDosisProgramadasCasoDeUso = obtenerDosisProgramadasCasoDeUso;
            _obtenerSectorEnfermeraCasoDeUso = obtenerSectorEnfermeraCasoDeUso;
        }

        [HttpGet("{id:guid}/panel")]
        [ProducesResponseType(typeof(IEnumerable<EnfermeraPanelDeControlRespuesta>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerPanelDeControl(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var resultado = await _obtenerPanelCasoDeUso.EjecutarAsync(id, cancellationToken);
            return Ok(resultado);
        }

        [HttpPut("{enfermeraId:guid}/dosis/{dosisId:guid}/administracion")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegistrarAdministracion(
            [FromRoute] Guid enfermeraId,
            [FromRoute] Guid dosisId,
            [FromBody] RegistrarAdministracionMedicacionSolicitud solicitud,
            CancellationToken cancellationToken)
        {
            await _registrarAdministracionCasoDeUso.EjecutarAsync(
                dosisId,
                enfermeraId,
                solicitud,
                cancellationToken);

            return NoContent();
        }

        [HttpPut("{enfermeraId:guid}/dosis/{dosisId:guid}/omision")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegistrarOmision(
            [FromRoute] Guid enfermeraId,
            [FromRoute] Guid dosisId,
            [FromBody] RegistrarOmisionMedicacionSolicitud solicitud,
            CancellationToken cancellationToken)
        {
            await _registrarOmisionCasoDeUso.EjecutarAsync(
                dosisId,
                enfermeraId,
                solicitud,
                cancellationToken);

            return NoContent();
        }

        [HttpGet("{enfermeraId:guid}/dosis-programadas")]
        [ProducesResponseType(typeof(PaginaRespuesta<DosisProgramadaRespuesta>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerDosisProgramadas(
            [FromRoute] Guid enfermeraId,
            [FromQuery] FiltroDosisProgramadasSolicitud filtro,
            CancellationToken cancellationToken)
        {
            var resultado = await _obtenerDosisProgramadasCasoDeUso.EjecutarAsync(
                enfermeraId,
                filtro,
                cancellationToken);

            return Ok(resultado);
        }

        [HttpGet("{enfermeraId:guid}/sector")]
        [ProducesResponseType(typeof(SectorEnfermeraRespuesta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SectorEnfermeraRespuesta>> ObtenerSector(
    [FromRoute] Guid enfermeraId,
    CancellationToken cancellationToken)
        {
            var respuesta = await _obtenerSectorEnfermeraCasoDeUso.EjecutarAsync(
                enfermeraId,
                cancellationToken);

            return Ok(respuesta);
        }
    }
}