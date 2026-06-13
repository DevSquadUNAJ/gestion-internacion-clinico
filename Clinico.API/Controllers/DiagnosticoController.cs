using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clinico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiagnosticosController : ControllerBase
{
    private readonly IRegistrarDiagnosticoCasoDeUso
        _registrarDiagnosticoCasoDeUso;

    public DiagnosticosController(
        IRegistrarDiagnosticoCasoDeUso registrarDiagnosticoCasoDeUso)
    {
        _registrarDiagnosticoCasoDeUso =
            registrarDiagnosticoCasoDeUso;
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegistrarDiagnosticoRespuesta),StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RegistrarDiagnosticoRespuesta>>RegistrarDiagnostico([FromBody] RegistrarDiagnosticoSolicitud solicitud)
    {
        var respuesta =await _registrarDiagnosticoCasoDeUso.EjecutarAsync(solicitud);

        return Created($"api/Diagnostico/{respuesta.DiagnosticoId}",respuesta);
    }
}