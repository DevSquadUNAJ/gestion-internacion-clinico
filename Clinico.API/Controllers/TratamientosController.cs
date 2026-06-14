using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Clinico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TratamientosController : ControllerBase
{
    private readonly IModificarTratamientoCasoDeUso _modificarTratamientoCasoDeUso;

    public TratamientosController(
        IModificarTratamientoCasoDeUso modificarTratamientoCasoDeUso)
    {
        _modificarTratamientoCasoDeUso = modificarTratamientoCasoDeUso;
    }

    [HttpPut("{Id}")]
    public async Task<ActionResult<ModificarTratamientoRespuesta>> Modificar(Guid Id, [FromBody] ModificarTratamientoSolicitud solicitud)
    {
        var respuesta = await _modificarTratamientoCasoDeUso.EjecutarAsync(Id, solicitud);

        return Ok(respuesta);
    }
}