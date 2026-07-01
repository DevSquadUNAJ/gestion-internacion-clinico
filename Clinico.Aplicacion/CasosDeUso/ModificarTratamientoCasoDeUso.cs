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

        var frecuencia = await _frecuenciaConsulta.ObtenerPorIdAsync(solicitud.FrecuenciaAdministracionId);
        if (frecuencia is null)
            throw new ExceptionNotFound("La frecuencia no existe");

        tratamiento.Dosis = solicitud.Dosis;
        tratamiento.FrecuenciaAdministracionId = solicitud.FrecuenciaAdministracionId;

        var dosisCanceladas = 0;

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
            await _tratamientoDosisComando.ActualizarAsync(dosisFuturas);
        }

        await _tratamientoComando.ActualizarAsync(tratamiento);

        return new ModificarTratamientoRespuesta
        {
            TratamientoId = tratamiento.Id,
            Dosis = tratamiento.Dosis,
            Frecuencia = frecuencia.Descripcion,
            Estado = tratamiento.Estado,
            DosisCanceladas = dosisCanceladas,
            FechaModificacion = DateTime.UtcNow
        };
    }
}