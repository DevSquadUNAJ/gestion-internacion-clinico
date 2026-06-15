using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso
{

    public sealed class RegistrarOmisionMedicacionCasoDeUso
        : IRegistrarOmisionMedicacionCasoDeUso
    {
        private readonly IObtenerTratamientoDosisConsulta _consulta;
        private readonly ITratamientoDosisComando _comando;

        public RegistrarOmisionMedicacionCasoDeUso(
            IObtenerTratamientoDosisConsulta consulta,
            ITratamientoDosisComando comando)
        {
            _consulta = consulta;
            _comando = comando;
        }

        public async Task EjecutarAsync(
            Guid dosisId,
            Guid enfermeraId,
            RegistrarOmisionMedicacionSolicitud solicitud,
            CancellationToken cancellationToken)
        {
            var dosis = await _consulta.ObtenerPorIdAsync(
                dosisId,
                cancellationToken);

            if (dosis is null)
                throw new Exception("La dosis no existe.");

            // El motivo es obligatorio
            if (string.IsNullOrWhiteSpace(solicitud.Motivo))
            {
                throw new InvalidOperationException(
                    "El motivo de omisión es obligatorio.");
            }

            // Solo pueden omitirse dosis pendientes
            if (dosis.Estado != EstadoDosis.Pendiente)
            {
                throw new InvalidOperationException(
                    "Solo pueden omitirse dosis pendientes.");
            }

            dosis.Estado = EstadoDosis.Omitida;
            dosis.MotivoOmision = solicitud.Motivo;
            dosis.Observaciones = solicitud.Observaciones;

            // Opcional: registrar quién realizó la acción
            dosis.EnfermeraId = enfermeraId;

            // Registrar fecha del sistema
            dosis.FechaDelSistema = DateTime.UtcNow;

            await _comando.ActualizarAsync(
                new List<TratamientoDosis> { dosis });
        }
    }
}
