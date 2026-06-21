using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Application.DTOs.Respuestas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Clinico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriaClinicaController : ControllerBase
    {
       private readonly IObtenerHistoriaClinicaCasoDeUso _obtenerHistoriaClinicaCasoDeUso;
        public HistoriaClinicaController(IObtenerHistoriaClinicaCasoDeUso obtenerHistoriaClinicaCasoDeUso)
        {
            _obtenerHistoriaClinicaCasoDeUso = obtenerHistoriaClinicaCasoDeUso;
        }

        [HttpGet("paciente/{Id}")]
        [ProducesResponseType(typeof(ObtenerHistoriaClinicaRespuesta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorApiRespuesta), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ObtenerHistoriaClinicaRespuesta>> ObtenerPorPaciente(Guid Id)
        {
            var resultado = await _obtenerHistoriaClinicaCasoDeUso.EjecutarAsync(Id);
            return Ok(resultado);
        }
    }
}
