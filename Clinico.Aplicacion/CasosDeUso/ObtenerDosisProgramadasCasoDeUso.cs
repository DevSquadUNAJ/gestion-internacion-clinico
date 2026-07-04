using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Aplicacion.Interfaces.IExternos;
using Clinico.Dominio.Constantes;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso
{
    public sealed class ObtenerDosisProgramadasCasoDeUso : IObtenerDosisProgramadasCasoDeUso
    {
        private readonly IObtenerEnfermeraConsulta _enfermeraConsulta;
        private readonly IAdmisionServicio _admisionServicio;
        private readonly IObtenerDosisProgramadasConsulta _dosisConsulta;

        public ObtenerDosisProgramadasCasoDeUso(
            IObtenerEnfermeraConsulta enfermeraConsulta,
            IAdmisionServicio admisionServicio,
            IObtenerDosisProgramadasConsulta dosisConsulta)
        {
            _enfermeraConsulta = enfermeraConsulta;
            _admisionServicio = admisionServicio;
            _dosisConsulta = dosisConsulta;
        }

        public async Task<PaginaRespuesta<DosisProgramadaRespuesta>> EjecutarAsync(
            Guid enfermeraId,
            FiltroDosisProgramadasSolicitud filtro,
            CancellationToken cancellationToken)
        {
            var enfermera = await _enfermeraConsulta.ObtenerPorIdAsync(enfermeraId, cancellationToken);
            if (enfermera is null)
                throw new ExceptionNotFound("La enfermera indicada no existe.");

            if (filtro.Pagina < 1)
                throw new ExceptionBadRequest("La página debe ser mayor o igual a 1.");

            if (filtro.TamPagina < 1)
                throw new ExceptionBadRequest("El tamaño de página debe ser mayor o igual a 1.");

            // Sector: el del filtro si vino, si no el de la enfermera.
            var sectorId = filtro.SectorId ?? enfermera.SectorId;

            // Refit a Admisión: resolver camas del sector → pacientes + datos de cama.
            var camas = await _admisionServicio.ObtenerCamasPorSectorAsync(sectorId);
            var camasOcupadas = camas.Where(c => c.PacienteId.HasValue).ToList();

            // Mapa pacienteId → datos de cama (para enriquecer sin N+1).
            var datosPorPaciente = camasOcupadas
                .GroupBy(c => c.PacienteId!.Value)
                .ToDictionary(g => g.Key, g => g.First());

            var pacientesIds = datosPorPaciente.Keys.ToList();

            // Filtro opcional por paciente puntual.
            if (filtro.PacienteId.HasValue)
            {
                pacientesIds = pacientesIds.Where(id => id == filtro.PacienteId.Value).ToList();
            }

            if (pacientesIds.Count == 0)
            {
                return new PaginaRespuesta<DosisProgramadaRespuesta>
                {
                    Elementos = Array.Empty<DosisProgramadaRespuesta>(),
                    PaginaActual = filtro.Pagina,
                    TamPagina = filtro.TamPagina,
                    TotalRegistros = 0,
                    TotalPaginas = 0
                };
            }

            var (dosis, totalRegistros) = await _dosisConsulta.ObtenerAsync(
                pacientesIds,
                filtro.Fecha,
                filtro.Estados,
                filtro.Pagina,
                filtro.TamPagina,
                filtro.FechaHoraDesde,
                filtro.FechaHoraHasta,
                cancellationToken);

            var elementos = dosis.Select(d =>
            {
                var pacienteId = d.Tratamiento.Diagnostico.HistoriaClinica.PacienteId;
                datosPorPaciente.TryGetValue(pacienteId, out var cama);

                return new DosisProgramadaRespuesta
                {
                    DosisId = d.Id,
                    PacienteId = pacienteId,
                    Paciente = cama?.PacienteAsignado ?? "Desconocido",
                    NumeroCama = cama?.Numero ?? 0,
                    Medicamento = d.Tratamiento.Medicamento.NombreComercial,
                    Dosis = d.Tratamiento.Dosis,
                    UnidadMedida = d.Tratamiento.UnidadMedida.Abreviatura,
                    FechaProgramada = d.FechaProgramada,
                    Estado = d.Estado,
                    Prioridad = d.FechaProgramada < DateTime.UtcNow
                        ? PrioridadDosis.Alta
                        : PrioridadDosis.Normal
                };
            }).ToList();

            return new PaginaRespuesta<DosisProgramadaRespuesta>
            {
                Elementos = elementos,
                PaginaActual = filtro.Pagina,
                TamPagina = filtro.TamPagina,
                TotalRegistros = totalRegistros,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)filtro.TamPagina)
            };
        }
    }
}