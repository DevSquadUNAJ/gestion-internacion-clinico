using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Constantes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso;

public class ModificarTratamientoCasoDeUso : IModificarTratamientoCasoDeUso
{
    private readonly ITratamientoConsulta _tratamientoConsulta;
    private readonly IFrecuenciaAdministracionConsulta _frecuenciaConsulta;
    private readonly ITratamientoDosisConsulta _tratamientoDosisConsulta;
    private readonly ITratamientoComando _tratamientoComando;
    private readonly ITratamientoDosisComando _tratamientoDosisComando;

    public ModificarTratamientoCasoDeUso(ITratamientoConsulta tratamientoConsulta, IFrecuenciaAdministracionConsulta frecuenciaConsulta, ITratamientoDosisConsulta tratamientoDosisConsulta, ITratamientoComando tratamientoComando, ITratamientoDosisComando tratamientoDosisComando)
    {
        _tratamientoConsulta = tratamientoConsulta;
        _frecuenciaConsulta = frecuenciaConsulta;
        _tratamientoDosisConsulta = tratamientoDosisConsulta;
        _tratamientoComando = tratamientoComando;
        _tratamientoDosisComando = tratamientoDosisComando;
    }

    public async Task<ModificarTratamientoRespuesta> EjecutarAsync(
        Guid tratamientoId,
        ModificarTratamientoSolicitud solicitud)
    {
        var tratamiento = await _tratamientoConsulta.ObtenerPorIdAsync(tratamientoId);
        if (tratamiento is null)
            throw new ExceptionNotFound("El tratamiento no Existe");

        if (tratamiento.Estado == EstadoTratamiento.Suspendido)
            throw new ExceptionBadRequest("El tratamiento ya se encuentra suspendido.");

        if (tratamiento.Estado == EstadoTratamiento.Finalizado)
            throw new ExceptionBadRequest("El tratamiento ya se encuentra finalizado.");

        string nombreFrecuencia = "No modificada";

        // 1. Modificar Frecuencia SOLO si fue enviada en la solicitud
        if (solicitud.FrecuenciaAdministracionId.HasValue)
        {
            var frecuencia = await _frecuenciaConsulta.ObtenerPorIdAsync(solicitud.FrecuenciaAdministracionId.Value);
            if (frecuencia is null)
                throw new ExceptionNotFound("La frecuencia no existe");

            tratamiento.FrecuenciaAdministracionId = solicitud.FrecuenciaAdministracionId.Value;
            nombreFrecuencia = frecuencia.Descripcion;
        }

        // 2. Modificar Dosis SOLO si fue enviada en la solicitud
        if (solicitud.Dosis.HasValue)
        {
            tratamiento.Dosis = solicitud.Dosis.Value;
        }

        var dosisCanceladas = 0;

        // 3. Lógica de Suspensión (Ya estaba perfecta)
        if (solicitud.Suspender)
        {
            tratamiento.Estado = EstadoTratamiento.Suspendido;

            var dosis = await _tratamientoDosisConsulta.ObtenerPorTratamientoAsync(tratamiento.Id);
            var dosisFuturas = dosis.Where(d => d.FechaProgramada > DateTime.UtcNow).ToList();

            foreach (var dosisFutura in dosisFuturas)
            {
                dosisFutura.Estado = EstadoDosis.Cancelada;
            }

            dosisCanceladas = dosisFuturas.Count;
            if (dosisFuturas.Any())
            {
                await _tratamientoDosisComando.ActualizarAsync(dosisFuturas);
            }
        }

        await _tratamientoComando.ActualizarAsync(tratamiento);

        return new ModificarTratamientoRespuesta
        {
            TratamientoId = tratamiento.Id,
            Dosis = tratamiento.Dosis,
            Frecuencia = nombreFrecuencia,
            Estado = tratamiento.Estado,
            DosisCanceladas = dosisCanceladas,
            FechaModificacion = DateTime.UtcNow
        };
    }
}