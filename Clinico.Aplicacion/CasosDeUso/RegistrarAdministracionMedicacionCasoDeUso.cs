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
    public sealed class RegistrarAdministracionMedicacionCasoDeUso : IRegistrarAdministracionMedicacionCasoDeUso
    {
        private readonly IObtenerTratamientoDosisConsulta _dosisConsulta;
        private readonly ITratamientoDosisComando _dosisComando;

        public RegistrarAdministracionMedicacionCasoDeUso(
            IObtenerTratamientoDosisConsulta dosisConsulta,
            ITratamientoDosisComando dosisComando)
        {
            _dosisConsulta = dosisConsulta;
            _dosisComando = dosisComando;
        }

        public async Task EjecutarAsync(
            Guid dosisId,
            Guid enfermeraId,
            RegistrarAdministracionMedicacionSolicitud solicitud,
            CancellationToken cancellationToken)
        {
            var dosis = await _dosisConsulta.ObtenerPorIdAsync(dosisId, cancellationToken);

            if (dosis is null)
                throw new ExceptionNotFound("La dosis indicada no existe.");

            if (dosis.Estado != EstadoDosis.Pendiente)
                throw new ExceptionBadRequest("Solo pueden administrarse dosis que estén en estado pendiente.");

            if (dosis.FechaSuministro.HasValue)
                throw new ExceptionBadRequest("Esta dosis ya fue administrada previamente.");

            dosis.FechaSuministro = solicitud.FechaSuministro;
            dosis.FechaDelSistema = DateTime.UtcNow;
            dosis.EnfermeraId = enfermeraId;
            dosis.Observaciones = solicitud.Observaciones;
            dosis.Estado = EstadoDosis.Administrada;

            await _dosisComando.ActualizarAsync(new List<TratamientoDosis> { dosis });
        }
    }
}