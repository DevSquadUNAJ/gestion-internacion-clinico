using Clinico.Aplicacion.CasosDeUso;
using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.API.Controllers
{
    [ApiController]
    [Route("api/nursing")]
    public sealed class EnfermeraController : ControllerBase
    {
        private readonly IObtenerEnfermeraPanelDeControlCasoDeUso _useCase;
        private readonly IRegistrarAdministracionMedicacionCasoDeUso _casoDeUso;
        private readonly IRegistrarOmisionMedicacionCasoDeUso _registrarOmisionCasoDeUso;

        public EnfermeraController(
            IObtenerEnfermeraPanelDeControlCasoDeUso useCase,
            IRegistrarAdministracionMedicacionCasoDeUso casoDeUso,
            IRegistrarOmisionMedicacionCasoDeUso registrarOmisionCasoDeUso)
        {
            _useCase = useCase;
            _casoDeUso = casoDeUso;
            _registrarOmisionCasoDeUso = registrarOmisionCasoDeUso;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> ObtenerPanelDeControl(
            CancellationToken cancellationToken)
        {
           /* var nurseId =
                User.GetUserId();*/
           var nurseId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            var result =
                await _useCase.EjecutarAsync(
                    nurseId,
                    cancellationToken);

            return Ok(result);
        }
        [HttpPut("dosis/{dosisId:guid}/administracion")]
        public async Task<IActionResult> RegistrarAdministracion(
        Guid dosisId,
        RegistrarAdministracionMedicacionSolicitud solicitud,
        CancellationToken cancellationToken)
            {
                // Temporal hasta integrar Seguridad
                var enfermeraId = Guid.Parse(
                    "GUID_DE_ENFERMERA");

                await _casoDeUso.EjecutarAsync(
                    dosisId,
                    enfermeraId,
                    solicitud,
                    cancellationToken);

                return NoContent();
            }

        [HttpPut("dosis/{dosisId:guid}/omision")]
        public async Task<IActionResult> RegistrarOmision(
        Guid dosisId,
        RegistrarOmisionMedicacionSolicitud solicitud,
        CancellationToken cancellationToken)
            {
                // Temporal hasta integrar Seguridad
                var enfermeraId = Guid.Parse(
                    "GUID_DE_ENFERMERA");

                await _registrarOmisionCasoDeUso.EjecutarAsync(
                    dosisId,
                    enfermeraId,
                    solicitud,
                    cancellationToken);

                return NoContent();
            }
    }
}
