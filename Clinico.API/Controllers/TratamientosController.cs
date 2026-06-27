using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.ISeguridad;
using Clinico.Application.DTOs.Respuestas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Medico")]
public class TratamientosController : ControllerBase
{
    private readonly IPrescribirTratamientoCasoDeUso _prescribirTratamientoCasoDeUso;
    private readonly IConfirmarTratamientoCasoDeUso _confirmarTratamientoCasoDeUso;
    private readonly ICancelarTratamientoCasoDeUso _cancelarTratamientoCasoDeUso;
    private readonly IModificarTratamientoCasoDeUso _modificarTratamientoCasoDeUso;
    private readonly IObtenerSeguimientoTratamientoCasoDeUso _obtenerSeguimientoTratamientoCasoDeUso;
    private readonly IMedicoActualServicio _medicoActualServicio;

    public TratamientosController(
        IPrescribirTratamientoCasoDeUso prescribirTratamientoCasoDeUso,
        IConfirmarTratamientoCasoDeUso confirmarTratamientoCasoDeUso,
        ICancelarTratamientoCasoDeUso cancelarTratamientoCasoDeUso,
        IModificarTratamientoCasoDeUso modificarTratamientoCasoDeUso,
        IObtenerSeguimientoTratamientoCasoDeUso obtenerSeguimientoTratamientoCasoDeUso,
        IMedicoActualServicio medicoActualServicio)
    {
        _prescribirTratamientoCasoDeUso = prescribirTratamientoCasoDeUso;
        _confirmarTratamientoCasoDeUso = confirmarTratamientoCasoDeUso;
        _cancelarTratamientoCasoDeUso = cancelarTratamientoCasoDeUso;
        _modificarTratamientoCasoDeUso = modificarTratamientoCasoDeUso;
        _obtenerSeguimientoTratamientoCasoDeUso = obtenerSeguimientoTratamientoCasoDeUso;
        _medicoActualServicio = medicoActualServicio;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PrescribirTratamientoRespuesta), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PrescribirTratamientoRespuesta>> Prescribir(
        [FromBody] PrescribirTratamientoSolicitud solicitud,
        CancellationToken cancellationToken)
    {
        var medicoId = _medicoActualServicio.ObtenerMedicoId();
        var respuesta = await _prescribirTratamientoCasoDeUso.EjecutarAsync(medicoId, solicitud, cancellationToken);
        return Created($"api/tratamientos/{respuesta.TratamientoId}", respuesta);
    }

    [HttpPost("{tratamientoId:guid}/confirmar")]
    [ProducesResponseType(typeof(ConfirmarTratamientoRespuesta), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ConfirmarTratamientoRespuesta>> Confirmar(
        [FromRoute] Guid tratamientoId,
        [FromBody] ConfirmarTratamientoSolicitud solicitud,
        CancellationToken cancellationToken)
    {
        var medicoId = _medicoActualServicio.ObtenerMedicoId();
        var respuesta = await _confirmarTratamientoCasoDeUso.EjecutarAsync(tratamientoId, medicoId, solicitud, cancellationToken);
        return Ok(respuesta);
    }

    [HttpPost("{tratamientoId:guid}/cancelar")]
    [ProducesResponseType(typeof(CancelarTratamientoRespuesta), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CancelarTratamientoRespuesta>> Cancelar(
        [FromRoute] Guid tratamientoId,
        CancellationToken cancellationToken)
    {
        var medicoId = _medicoActualServicio.ObtenerMedicoId();
        var respuesta = await _cancelarTratamientoCasoDeUso.EjecutarAsync(tratamientoId, medicoId, cancellationToken);
        return Ok(respuesta);
    }

    [HttpPut("{tratamientoId:guid}")]
    [ProducesResponseType(typeof(ModificarTratamientoRespuesta), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ModificarTratamientoRespuesta>> Modificar(
        Guid tratamientoId,
        [FromBody] ModificarTratamientoSolicitud solicitud)
    {
        var respuesta = await _modificarTratamientoCasoDeUso.EjecutarAsync(tratamientoId, solicitud);
        return Ok(respuesta);
    }

    [HttpGet("{tratamientoId:guid}/seguimiento")]
    [ProducesResponseType(typeof(ObtenerSeguimientoTratamientoRespuesta), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ObtenerSeguimientoTratamientoRespuesta>> ObtenerSeguimiento(
        Guid tratamientoId)
    {
        var respuesta = await _obtenerSeguimientoTratamientoCasoDeUso.EjecutarAsync(tratamientoId);
        return Ok(respuesta);
    }
}