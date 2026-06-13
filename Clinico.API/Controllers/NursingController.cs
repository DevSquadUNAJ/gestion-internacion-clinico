using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Clinico.Aplicacion.CasosDeUso;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.DTOs.Respuestas;
using System;

namespace Clinico.API.Controllers
{
    [ApiController]
    [Route("api/nursing")]
    public sealed class NursingController : ControllerBase
    {
        private readonly IGetNursingDashboardUseCase _useCase;

        public NursingController(IGetNursingDashboardUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard(
            CancellationToken cancellationToken)
        {
           /* var nurseId =
                User.GetUserId();*/
           var nurseId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            var result =
                await _useCase.ExecuteAsync(
                    nurseId,
                    cancellationToken);

            return Ok(result);
        }
    }
}
