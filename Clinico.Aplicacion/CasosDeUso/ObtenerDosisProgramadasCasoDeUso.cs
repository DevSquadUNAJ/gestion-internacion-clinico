using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IConsultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso
{
    public sealed class ObtenerDosisProgramadasCasoDeUso
        : IObtenerDosisProgramadasCasoDeUso
    {
        private readonly IObtenerEnfermeraConsulta _consultaEnfermera;
        private readonly IObtenerDosisProgramadasConsulta _consulta;

        public ObtenerDosisProgramadasCasoDeUso(
            IObtenerEnfermeraConsulta consultaEnfermera,
            IObtenerDosisProgramadasConsulta consulta)
        {
            _consultaEnfermera = consultaEnfermera;
            _consulta = consulta;
        }

        public async Task<PaginaRespuesta<DosisProgramadaRespuesta>>
            EjecutarAsync(
                Guid enfermeraId,
                DateTime fecha,
                int pagina,
                int tamPagina,
                CancellationToken cancellationToken)
        {
            var enfermera =
                await _consultaEnfermera.ObtenerPorIdAsync(
                    enfermeraId,
                    cancellationToken);

            if (enfermera is null)
                throw new Exception("La enfermera no existe.");

            return await _consulta.ObtenerAsync(
                enfermera.SectorId,
                fecha,
                pagina,
                tamPagina,
                cancellationToken);
        }
    }
}
