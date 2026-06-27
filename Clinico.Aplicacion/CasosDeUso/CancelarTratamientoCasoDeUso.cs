using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Constantes;
using Clinico.Dominio.Entidades;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso
{
    public sealed class CancelarTratamientoCasoDeUso : ICancelarTratamientoCasoDeUso
    {
        private readonly ITratamientoConsulta _tratamientoConsulta;
        private readonly IMedicoConsulta _medicoConsulta;
        private readonly ITratamientoComando _tratamientoComando;
        private readonly IAuditoriaComando _auditoriaComando;

        public CancelarTratamientoCasoDeUso(
            ITratamientoConsulta tratamientoConsulta,
            IMedicoConsulta medicoConsulta,
            ITratamientoComando tratamientoComando,
            IAuditoriaComando auditoriaComando)
        {
            _tratamientoConsulta = tratamientoConsulta;
            _medicoConsulta = medicoConsulta;
            _tratamientoComando = tratamientoComando;
            _auditoriaComando = auditoriaComando;
        }

        public async Task<CancelarTratamientoRespuesta> EjecutarAsync(
            Guid tratamientoId,
            Guid medicoId,
            CancellationToken cancellationToken)
        {
            var medico = await _medicoConsulta.ObtenerPorIdAsync(medicoId)
                ?? throw new ExceptionUnauthorized("El médico autenticado no existe en el sistema clínico.");

            var tratamiento = await _tratamientoConsulta.ObtenerPorIdParaActualizarAsync(tratamientoId)
                ?? throw new ExceptionNotFound("El tratamiento indicado no existe.");

            if (tratamiento.Estado != EstadoTratamiento.PendienteValidacion)
                throw new ExceptionBadRequest("Solo pueden cancelarse tratamientos en estado PendienteValidacion.");

            tratamiento.Estado = EstadoTratamiento.Cancelado;
            await _tratamientoComando.ActualizarAsync(tratamiento);

            var auditoria = new Auditoria
            {
                Id = Guid.NewGuid(),
                UsuarioId = medicoId.ToString(),
                Rol = "Medico",
                Accion = "CancelarTratamiento",
                Entidad = nameof(Tratamiento),
                EntidadId = tratamiento.Id,
                Descripcion = "Tratamiento en PendienteValidacion cancelado por el médico.",
                PayloadJson = JsonSerializer.Serialize(new { tratamiento.Id }),
                FechaHora = DateTime.UtcNow
            };
            await _auditoriaComando.AgregarAsync(auditoria);

            return new CancelarTratamientoRespuesta
            {
                TratamientoId = tratamiento.Id,
                Estado = tratamiento.Estado,
                FechaCancelacion = DateTime.UtcNow
            };
        }
    }
}