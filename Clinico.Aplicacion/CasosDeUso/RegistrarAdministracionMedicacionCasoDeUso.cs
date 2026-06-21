using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Dominio.Constantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso
{

    public sealed class RegistrarAdministracionMedicacionCasoDeUso
        : IRegistrarAdministracionMedicacionCasoDeUso
    {
        private readonly IObtenerTratamientoDosisConsulta _consulta;
       
        private readonly ITratamientoDosisComando _tratamientoDosisComando;

        public RegistrarAdministracionMedicacionCasoDeUso(
            IObtenerTratamientoDosisConsulta consulta,
            ITratamientoDosisComando tratamientoDosisComando)
        {
            _consulta = consulta;
            _tratamientoDosisComando = tratamientoDosisComando;
        }

        public async Task EjecutarAsync(
            Guid dosisId,
            Guid enfermeraId,
            RegistrarAdministracionMedicacionSolicitud solicitud,
            CancellationToken cancellationToken)
        {
            var dosis = await _consulta.ObtenerPorIdAsync(
                dosisId,
                cancellationToken);

            if (dosis is null)
                throw new Exception("La dosis no existe.");

            // Solo pueden administrarse dosis pendientes
            if (dosis.Estado != EstadoDosis.Pendiente)
            {
                throw new InvalidOperationException(
                    "Solo pueden administrarse dosis pendientes.");
            }

            // No puede administrarse dos veces
            if (dosis.FechaSuministro.HasValue)
            {
                throw new InvalidOperationException(
                    "La dosis ya fue administrada.");
            }

            dosis.FechaSuministro = solicitud.SuppliedAt;
            dosis.FechaDelSistema = DateTime.UtcNow;
            dosis.EnfermeraId = enfermeraId;
            dosis.Observaciones = solicitud.Observations;
            dosis.Estado = EstadoDosis.Administrada;

            await _tratamientoDosisComando.ActualizarAsync(
                new List<TratamientoDosis> { dosis });
        }
    }
}
