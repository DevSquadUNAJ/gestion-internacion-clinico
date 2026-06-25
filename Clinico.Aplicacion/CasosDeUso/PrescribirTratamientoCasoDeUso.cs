using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Aplicacion.Interfaces.IExternos;
using Clinico.Aplicacion.Interfaces.IMapeadores;
using Clinico.Dominio.Constantes;
using Clinico.Dominio.Entidades;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso
{
    public class PrescribirTratamientoCasoDeUso : IPrescribirTratamientoCasoDeUso
    {
        private readonly IDiagnosticoConsulta _diagnosticoConsulta;
        private readonly IHistoriaClinicaConsulta _historiaClinicaConsulta;
        private readonly IMedicoConsulta _medicoConsulta;
        private readonly IMedicamentoConsulta _medicamentoConsulta;
        private readonly IUnidadMedidaConsulta _unidadMedidaConsulta;
        private readonly IFrecuenciaAdministracionConsulta _frecuenciaConsulta;
        private readonly ITratamientoComando _tratamientoComando;
        private readonly IAuditoriaIAComando _auditoriaIAComando;
        private readonly IValidadorClinicoIA _validadorClinicoIA;
        private readonly IPrescribirTratamientoMapeador _mapeador;

        public PrescribirTratamientoCasoDeUso(
            IDiagnosticoConsulta diagnosticoConsulta,
            IHistoriaClinicaConsulta historiaClinicaConsulta,
            IMedicoConsulta medicoConsulta,
            IMedicamentoConsulta medicamentoConsulta,
            IUnidadMedidaConsulta unidadMedidaConsulta,
            IFrecuenciaAdministracionConsulta frecuenciaConsulta,
            ITratamientoComando tratamientoComando,
            IAuditoriaIAComando auditoriaIAComando,
            IValidadorClinicoIA validadorClinicoIA,
            IPrescribirTratamientoMapeador mapeador)
        {
            _diagnosticoConsulta = diagnosticoConsulta;
            _historiaClinicaConsulta = historiaClinicaConsulta;
            _medicoConsulta = medicoConsulta;
            _medicamentoConsulta = medicamentoConsulta;
            _unidadMedidaConsulta = unidadMedidaConsulta;
            _frecuenciaConsulta = frecuenciaConsulta;
            _tratamientoComando = tratamientoComando;
            _auditoriaIAComando = auditoriaIAComando;
            _validadorClinicoIA = validadorClinicoIA;
            _mapeador = mapeador;
        }

        public async Task<PrescribirTratamientoRespuesta> EjecutarAsync(
            Guid medicoId,
            PrescribirTratamientoSolicitud solicitud,
            CancellationToken cancellationToken)
        {
            // 1. VALIDACIONES DE EXISTENCIA

            var medico = await _medicoConsulta.ObtenerPorIdAsync(medicoId)
                ?? throw new ExceptionUnauthorized("El médico autenticado no existe en el sistema clínico.");

            var diagnostico = await _diagnosticoConsulta.ObtenerPorIdAsync(solicitud.DiagnosticoId)
                ?? throw new ExceptionNotFound("El diagnóstico indicado no existe.");

            var medicamento = await _medicamentoConsulta.ObtenerPorIdAsync(solicitud.MedicamentoId)
                ?? throw new ExceptionNotFound("El medicamento indicado no existe.");

            var unidadMedida = await _unidadMedidaConsulta.ObtenerPorIdAsync(solicitud.UnidadMedidaId)
                ?? throw new ExceptionNotFound("La unidad de medida indicada no existe.");

            var frecuencia = await _frecuenciaConsulta.ObtenerPorIdAsync(solicitud.FrecuenciaAdministracionId)
                ?? throw new ExceptionNotFound("La frecuencia de administración indicada no existe.");


            // 2. VALIDACIONES DE NEGOCIO

            if (solicitud.Dosis <= 0)
                throw new ExceptionBadRequest("La dosis debe ser mayor a cero.");

            if (solicitud.FechaInicio >= solicitud.FechaFin)
                throw new ExceptionBadRequest("La fecha de inicio debe ser anterior a la fecha de fin.");

            if (solicitud.FechaInicio < DateTime.UtcNow.AddMinutes(-1))
                throw new ExceptionBadRequest("La fecha de inicio no puede estar en el pasado.");

            var historiaClinica = await _historiaClinicaConsulta.ObtenerPorPacienteIdAsync(
                diagnostico.HistoriaClinica.PacienteId)
                ?? throw new ExceptionNotFound("La historia clínica asociada al diagnóstico no existe.");

            // 3. CREACIÓN Y PERSISTENCIA DEL TRATAMIENTO

            var tratamiento = new Tratamiento
            {
                Id = Guid.NewGuid(),
                DiagnosticoId = diagnostico.Id,
                MedicamentoId = medicamento.Id,
                UnidadMedidaId = unidadMedida.Id,
                FrecuenciaAdministracionId = frecuencia.Id,
                Dosis = solicitud.Dosis,
                FechaInicio = solicitud.FechaInicio,
                FechaFin = solicitud.FechaFin,
                Estado = EstadoTratamiento.PendienteValidacion,
                Observaciones = solicitud.Observaciones
            };

            await _tratamientoComando.AgregarAsync(tratamiento);


            // 4. ARMADO DEL CONTEXTO E INVOCACIÓN DE LA IA

            var contextoIA = _mapeador.MapearContextoClinico(
                historiaClinica,
                diagnostico,
                medicamento,
                unidadMedida,
                frecuencia,
                tratamiento);

            var resultadoIA = await _validadorClinicoIA.AnalizarAsync(contextoIA, cancellationToken);

            // 5. PERSISTENCIA DE LA AUDITORÍA IA (si hubo respuesta)

            var estadoAnalisis = EstadoAnalisisIA.Completado;

            if (resultadoIA.Exitoso && resultadoIA.Analisis is not null)
            {
                var auditoriaIA = new AuditoriaIA
                {
                    Id = Guid.NewGuid(),
                    TratamientoId = tratamiento.Id,
                    AlertaDetectada = resultadoIA.Analisis.AlertaDetectada,
                    MensajeIA = resultadoIA.PayloadJsonCrudo ?? string.Empty,
                    FueForzado = false,
                    JustificacionClinica = null,
                    FechaHora = DateTime.UtcNow
                };

                await _auditoriaIAComando.AgregarAsync(auditoriaIA);

            }
            else
            {
                estadoAnalisis = resultadoIA.FueTimeout
                    ? EstadoAnalisisIA.TimeoutExcedido
                    : EstadoAnalisisIA.NoDisponible;
            }


            return _mapeador.Mapear(tratamiento, resultadoIA.Analisis, estadoAnalisis);
        }
    }
}