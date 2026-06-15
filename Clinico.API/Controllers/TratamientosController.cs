using Clinico.Aplicacion.CasosDeUso;
using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Clinico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TratamientosController : ControllerBase
{
    private readonly IModificarTratamientoCasoDeUso _modificarTratamientoCasoDeUso;
    private readonly IObtenerSeguimientoTratamientoCasoDeUso _obtenerSeguimientoTratamientoCasoDeUso;

    public TratamientosController(IModificarTratamientoCasoDeUso modificarTratamientoCasoDeUso, IObtenerSeguimientoTratamientoCasoDeUso obtenerSeguimientoTratamientoCasoDeUso)
    {
        _modificarTratamientoCasoDeUso = modificarTratamientoCasoDeUso;
        _obtenerSeguimientoTratamientoCasoDeUso = obtenerSeguimientoTratamientoCasoDeUso;
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(typeof(ModificarTratamientoRespuesta),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionBadRequest), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionNotFound), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModificarTratamientoRespuesta>> Modificar(Guid Id, [FromBody] ModificarTratamientoSolicitud solicitud)
    {
        var respuesta = await _modificarTratamientoCasoDeUso.EjecutarAsync(Id, solicitud);
        return Ok(respuesta);
    }

    [HttpGet("{Id}/seguimiento")]
    [ProducesResponseType(typeof(ObtenerSeguimientoTratamientoRespuesta), StatusCodes.Status200OK)]
    public async Task<ActionResult<ObtenerSeguimientoTratamientoRespuesta>> ObtenerSeguimiento(Guid Id)
    {
        var respuesta =await _obtenerSeguimientoTratamientoCasoDeUso.EjecutarAsync(Id);
        return Ok(respuesta);
    }
}