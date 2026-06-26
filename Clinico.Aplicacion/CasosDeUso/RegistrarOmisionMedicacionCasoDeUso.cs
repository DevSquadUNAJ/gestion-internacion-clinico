using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Dominio.Constantes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso
{
    public class RegistrarOmisionMedicacionCasoDeUso : IRegistrarOmisionMedicacionCasoDeUso
    {
        private readonly IObtenerTratamientoDosisConsulta _dosisConsulta;
        private readonly ITratamientoDosisComando _dosisComando;

        public RegistrarOmisionMedicacionCasoDeUso(
            IObtenerTratamientoDosisConsulta dosisConsulta,
            ITratamientoDosisComando dosisComando)
        {
            _dosisConsulta = dosisConsulta;
            _dosisComando = dosisComando;
        }

        public async Task EjecutarAsync(
            Guid dosisId,
            Guid enfermeraId,
            RegistrarOmisionMedicacionSolicitud solicitud,
            CancellationToken cancellationToken)
        {
            var dosis = await _dosisConsulta.ObtenerPorIdAsync(dosisId, cancellationToken);

            if (dosis is null)
                throw new ExceptionNotFound("La dosis indicada no existe.");

            if (dosis.Estado != EstadoDosis.Pendiente)
                throw new ExceptionBadRequest("Solo pueden omitirse dosis que estén en estado pendiente.");

            if (string.IsNullOrWhiteSpace(solicitud.Motivo))
                throw new ExceptionBadRequest("Debe indicar un motivo obligatorio para la omisión de la medicación.");

            dosis.FechaDelSistema = DateTime.UtcNow;
            dosis.EnfermeraId = enfermeraId;
            dosis.MotivoOmision = solicitud.Motivo;
            dosis.Observaciones = solicitud.Observaciones;
            dosis.Estado = EstadoDosis.Omitida;

            await _dosisComando.ActualizarAsync(new List<TratamientoDosis> { dosis });
        }
    }
}