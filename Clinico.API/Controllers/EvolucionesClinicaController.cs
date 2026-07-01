using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Application.DTOs.Respuestas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clinico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Medico")]
public class EvolucionesClinicasController : ControllerBase
{
    private readonly IRegistrarEvolucionClinicaCasoDeUso _registrarEvolucionClinicaCasoDeUso;

    public EvolucionesClinicasController(IRegistrarEvolucionClinicaCasoDeUso registrarEvolucionClinicaCasoDeUso)
    {
        _registrarEvolucionClinicaCasoDeUso = registrarEvolucionClinicaCasoDeUso;
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegistrarEvolucionClinicaRespuesta), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RegistrarEvolucionClinicaRespuesta>> RegistrarEvolucionClinica([FromBody] RegistrarEvolucionClinicaSolicitud solicitud)
    {
        var respuesta = await _registrarEvolucionClinicaCasoDeUso.EjecutarAsync(solicitud);

        return Created($"api/EvolucionesClinicas/{respuesta.EvolucionClinicaId}", respuesta);
    }
}