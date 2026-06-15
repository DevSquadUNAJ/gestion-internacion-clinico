using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso;

public class RegistrarEvolucionClinicaCasoDeUso : IRegistrarEvolucionClinicaCasoDeUso
{
    private readonly IHistoriaClinicaConsulta _historiaClinicaConsulta;
    private readonly IMedicoConsulta _medicoConsulta;
    private readonly IEvolucionClinicaComando _evolucionClinicaComando;

    public RegistrarEvolucionClinicaCasoDeUso(
        IHistoriaClinicaConsulta historiaClinicaConsulta,
        IMedicoConsulta medicoConsulta,
        IEvolucionClinicaComando evolucionClinicaComando)
    {
        _historiaClinicaConsulta = historiaClinicaConsulta;
        _medicoConsulta = medicoConsulta;
        _evolucionClinicaComando = evolucionClinicaComando;
    }

    public async Task<RegistrarEvolucionClinicaRespuesta> EjecutarAsync(RegistrarEvolucionClinicaSolicitud solicitud)
    {
        var historiaClinica = await _historiaClinicaConsulta.ObtenerPorIdAsync(solicitud.HistoriaClinicaId);
        if (historiaClinica is null)
            throw new ExceptionNotFound("La historia clínica indicada no existe.");

        var medico = await _medicoConsulta.ObtenerPorIdAsync(solicitud.MedicoId);
        if (medico is null)
            throw new ExceptionNotFound("El médico indicado no existe.");

        var evolucionClinica = new EvolucionClinica
        {
            Id = Guid.NewGuid(),
            HistoriaClinicaId = historiaClinica.Id,
            MedicoId = medico.Id,
            FechaHora = DateTime.UtcNow,
            Observacion = solicitud.Observacion
        };

        await _evolucionClinicaComando.AgregarAsync(evolucionClinica);

        return new RegistrarEvolucionClinicaRespuesta
        {
            EvolucionClinicaId = evolucionClinica.Id,
            FechaHora = evolucionClinica.FechaHora
        };
    }
}