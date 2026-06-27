using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Constantes;
using Clinico.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso
{
    public sealed class ConfirmarTratamientoCasoDeUso : IConfirmarTratamientoCasoDeUso
    {
        private readonly ITratamientoConsulta _tratamientoConsulta;
        private readonly IMedicamentoConsulta _medicamentoConsulta;
        private readonly IUnidadMedidaConsulta _unidadMedidaConsulta;
        private readonly IFrecuenciaAdministracionConsulta _frecuenciaConsulta;
        private readonly IMedicoConsulta _medicoConsulta;
        private readonly IAuditoriaIAConsulta _auditoriaIAConsulta;
        private readonly ITratamientoComando _tratamientoComando;
        private readonly ITratamientoDosisComando _tratamientoDosisComando;
        private readonly IAuditoriaIAComando _auditoriaIAComando;
        private readonly IAuditoriaComando _auditoriaComando;

        public ConfirmarTratamientoCasoDeUso(
            ITratamientoConsulta tratamientoConsulta,
            IMedicamentoConsulta medicamentoConsulta,
            IUnidadMedidaConsulta unidadMedidaConsulta,
            IFrecuenciaAdministracionConsulta frecuenciaConsulta,
            IMedicoConsulta medicoConsulta,
            IAuditoriaIAConsulta auditoriaIAConsulta,
            ITratamientoComando tratamientoComando,
            ITratamientoDosisComando tratamientoDosisComando,
            IAuditoriaIAComando auditoriaIAComando,
            IAuditoriaComando auditoriaComando)
        {
            _tratamientoConsulta = tratamientoConsulta;
            _medicamentoConsulta = medicamentoConsulta;
            _unidadMedidaConsulta = unidadMedidaConsulta;
            _frecuenciaConsulta = frecuenciaConsulta;
            _medicoConsulta = medicoConsulta;
            _auditoriaIAConsulta = auditoriaIAConsulta;
            _tratamientoComando = tratamientoComando;
            _tratamientoDosisComando = tratamientoDosisComando;
            _auditoriaIAComando = auditoriaIAComando;
            _auditoriaComando = auditoriaComando;
        }

        public async Task<ConfirmarTratamientoRespuesta> EjecutarAsync(
            Guid tratamientoId,
            Guid medicoId,
            ConfirmarTratamientoSolicitud solicitud,
            CancellationToken cancellationToken)
        {
            var medico = await _medicoConsulta.ObtenerPorIdAsync(medicoId)
                ?? throw new ExceptionUnauthorized("El médico autenticado no existe en el sistema clínico.");

            var tratamiento = await _tratamientoConsulta.ObtenerPorIdParaActualizarAsync(tratamientoId)
                ?? throw new ExceptionNotFound("El tratamiento indicado no existe.");

            if (tratamiento.Estado != EstadoTratamiento.PendienteValidacion)
                throw new ExceptionBadRequest("Solo pueden confirmarse tratamientos en estado PendienteValidacion.");

            var medicamento = await _medicamentoConsulta.ObtenerPorIdAsync(solicitud.MedicamentoId)
                ?? throw new ExceptionNotFound("El medicamento indicado no existe.");

            var unidadMedida = await _unidadMedidaConsulta.ObtenerPorIdAsync(solicitud.UnidadMedidaId)
                ?? throw new ExceptionNotFound("La unidad de medida indicada no existe.");

            var frecuencia = await _frecuenciaConsulta.ObtenerPorIdAsync(solicitud.FrecuenciaAdministracionId)
                ?? throw new ExceptionNotFound("La frecuencia de administración indicada no existe.");

            if (solicitud.Dosis <= 0)
                throw new ExceptionBadRequest("La dosis debe ser mayor a cero.");

            if (solicitud.FechaInicio >= solicitud.FechaFin)
                throw new ExceptionBadRequest("La fecha de inicio debe ser anterior a la fecha de fin.");

            if (frecuencia.CantidadHoras <= 0)
                throw new ExceptionBadRequest("La frecuencia de administración tiene una cantidad de horas inválida.");

            var auditoriaIA = await _auditoriaIAConsulta
                .ObtenerUltimaPorTratamientoParaActualizarAsync(tratamiento.Id);

            var requiereJustificacion = auditoriaIA is not null
                && (auditoriaIA.NivelRiesgo == NivelRiesgoIA.Alto
                    || auditoriaIA.NivelRiesgo == NivelRiesgoIA.Critico);

            if (requiereJustificacion && string.IsNullOrWhiteSpace(solicitud.JustificacionClinica))
                throw new ExceptionBadRequest(
                    "El análisis de IA detectó un riesgo Alto o Crítico. Debe indicar una justificación clínica para confirmar el tratamiento.");

            tratamiento.MedicamentoId = medicamento.Id;
            tratamiento.UnidadMedidaId = unidadMedida.Id;
            tratamiento.FrecuenciaAdministracionId = frecuencia.Id;
            tratamiento.Dosis = solicitud.Dosis;
            tratamiento.FechaInicio = solicitud.FechaInicio;
            tratamiento.FechaFin = solicitud.FechaFin;
            tratamiento.Observaciones = solicitud.Observaciones;
            tratamiento.Estado = EstadoTratamiento.Activo;

            await _tratamientoComando.ActualizarAsync(tratamiento);

            var dosis = GenerarDosis(tratamiento, frecuencia.CantidadHoras);
            if (dosis.Count > 0)
                await _tratamientoDosisComando.AgregarRangoAsync(dosis);

            if (auditoriaIA is not null)
            {
                auditoriaIA.FueForzado = solicitud.FueForzado || requiereJustificacion;
                auditoriaIA.JustificacionClinica = solicitud.JustificacionClinica;
                await _auditoriaIAComando.ActualizarAsync(auditoriaIA);
            }

            var auditoria = new Auditoria
            {
                Id = Guid.NewGuid(),
                UsuarioId = medicoId.ToString(),
                Rol = "Medico",
                Accion = "ConfirmarTratamiento",
                Entidad = nameof(Tratamiento),
                EntidadId = tratamiento.Id,
                Descripcion = $"Tratamiento confirmado y activado. Dosis generadas: {dosis.Count}.",
                PayloadJson = JsonSerializer.Serialize(new
                {
                    solicitud.MedicamentoId,
                    solicitud.Dosis,
                    solicitud.FechaInicio,
                    solicitud.FechaFin,
                    solicitud.FueForzado,
                    DosisGeneradas = dosis.Count
                }),
                FechaHora = DateTime.UtcNow
            };
            await _auditoriaComando.AgregarAsync(auditoria);

            return new ConfirmarTratamientoRespuesta
            {
                TratamientoId = tratamiento.Id,
                Estado = tratamiento.Estado,
                CantidadDosisGeneradas = dosis.Count,
                PrimeraDosis = dosis.Count > 0 ? dosis.First().FechaProgramada : null,
                UltimaDosis = dosis.Count > 0 ? dosis.Last().FechaProgramada : null,
                FechaConfirmacion = DateTime.UtcNow
            };
        }

        private static List<TratamientoDosis> GenerarDosis(Tratamiento tratamiento, int cantidadHoras)
        {
            var dosis = new List<TratamientoDosis>();
            var fechaActual = tratamiento.FechaInicio;

            while (fechaActual <= tratamiento.FechaFin)
            {
                dosis.Add(new TratamientoDosis
                {
                    Id = Guid.NewGuid(),
                    TratamientoId = tratamiento.Id,
                    EnfermeraId = null,
                    FechaProgramada = fechaActual,
                    FechaSuministro = null,
                    FechaDelSistema = null,
                    Estado = EstadoDosis.Pendiente,
                    MotivoOmision = null,
                    Observaciones = null
                });

                fechaActual = fechaActual.AddHours(cantidadHoras);
            }

            return dosis;
        }
    }
}