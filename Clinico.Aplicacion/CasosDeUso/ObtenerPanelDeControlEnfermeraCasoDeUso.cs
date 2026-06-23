using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Aplicacion.Interfaces.IExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso
{
    public class ObtenerPanelDeControlEnfermeraCasoDeUso : IObtenerEnfermeraPanelDeControlCasoDeUso
    {
        private readonly IObtenerEnfermeraConsulta _enfermeraConsulta;
        private readonly IAdmisionServicio _admisionServicio;
        private readonly ITratamientoDosisConsulta _dosisConsulta;

        public ObtenerPanelDeControlEnfermeraCasoDeUso(
            IObtenerEnfermeraConsulta enfermeraConsulta,
            IAdmisionServicio admisionServicio,
            ITratamientoDosisConsulta dosisConsulta)
        {
            _enfermeraConsulta = enfermeraConsulta;
            _admisionServicio = admisionServicio;
            _dosisConsulta = dosisConsulta;
        }

        // Soluciona el Error 1: Agregamos IReadOnlyCollection y el CancellationToken
        public async Task<IReadOnlyCollection<EnfermeraPanelDeControlRespuesta>> EjecutarAsync(
            Guid enfermeraId,
            CancellationToken cancellationToken)
        {
            // Soluciona el Error 2: Le pasamos el cancellationToken a la consulta de la enfermera
            var enfermera = await _enfermeraConsulta.ObtenerPorIdAsync(enfermeraId, cancellationToken);

            if (enfermera is null)
                throw new ExceptionNotFound("La enfermera indicada no existe.");

            // MAGIA REFIT: Le preguntamos a Admisión por las camas de ese sector
            var camasDelSector = await _admisionServicio.ObtenerCamasPorSectorAsync(enfermera.SectorId);

            // Filtramos solo las camas que tienen un paciente acostado ahí
            var camasOcupadas = camasDelSector.Where(c => c.PacienteId.HasValue).ToList();
            var pacientesIds = camasOcupadas.Select(c => c.PacienteId!.Value).ToList();

            // Si el sector está vacío, el tablero no tiene nada pendiente
            if (!pacientesIds.Any())
                return new List<EnfermeraPanelDeControlRespuesta>();

            // Buscamos las dosis pendientes SÓLO para los pacientes de ese sector
            var dosisPendientes = await _dosisConsulta.ObtenerPendientesPorPacientesAsync(pacientesIds);

            // Cruzamos los datos y mapeamos
            return dosisPendientes.Select(d =>
            {
                var pacienteId = d.Tratamiento.Diagnostico.HistoriaClinica.PacienteId;
                var datosCama = camasOcupadas.First(c => c.PacienteId == pacienteId);

                return new EnfermeraPanelDeControlRespuesta
                {
                    DosisId = d.Id,
                    NumeroCama = datosCama.Numero,
                    Paciente = datosCama.PacienteAsignado ?? "Desconocido",
                    Medicamento = d.Tratamiento.Medicamento.NombreComercial,
                    FechaProgramada = d.FechaProgramada,
                    Prioridad = d.FechaProgramada < DateTime.UtcNow ? "Alta" : "Normal"
                };
            })
            .OrderByDescending(x => x.Prioridad == "Alta")
            .ThenBy(x => x.FechaProgramada)
            .ToList(); // <-- Soluciona el Error 1: Convierte el IEnumerable a una lista para cumplir con IReadOnlyCollection
        }
    }
}