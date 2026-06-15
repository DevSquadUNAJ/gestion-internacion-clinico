using Clinico.Aplicacion.DTOs.Solicitudes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.ICasosDeUso
{
    public interface IRegistrarOmisionMedicacionCasoDeUso
    {
        Task EjecutarAsync(
            Guid dosisId,
            Guid enfermeraId,
            RegistrarOmisionMedicacionSolicitud solicitud,
            CancellationToken cancellationToken);
    }
}
