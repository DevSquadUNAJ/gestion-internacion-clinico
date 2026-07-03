using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Aplicacion.Interfaces.IExternos;
using Clinico.Aplicacion.Interfaces.IMapeadores;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso
{
    public sealed class ObtenerSectorEnfermeraCasoDeUso : IObtenerSectorEnfermeraCasoDeUso
    {
        private readonly IObtenerEnfermeraConsulta _enfermeraConsulta;
        private readonly IAdmisionServicio _admisionServicio;
        private readonly ISectorEnfermeraMapeador _mapeador;

        public ObtenerSectorEnfermeraCasoDeUso(
            IObtenerEnfermeraConsulta enfermeraConsulta,
            IAdmisionServicio admisionServicio,
            ISectorEnfermeraMapeador mapeador)
        {
            _enfermeraConsulta = enfermeraConsulta;
            _admisionServicio = admisionServicio;
            _mapeador = mapeador;
        }

        public async Task<SectorEnfermeraRespuesta> EjecutarAsync(
            Guid enfermeraId,
            CancellationToken cancellationToken)
        {
            var enfermera = await _enfermeraConsulta.ObtenerPorIdAsync(enfermeraId, cancellationToken);
            if (enfermera is null)
                throw new ExceptionNotFound("La enfermera indicada no existe.");

            // TODO(deuda técnica): Admisión no expone GET /api/sectores/{sectorId}.
            // Traemos todos los sectores y filtramos en memoria. Refactorizar cuando
            // exista el endpoint puntual.
            var sectores = await _admisionServicio.ObtenerSectoresAsync();
            var sector = sectores.FirstOrDefault(s => s.SectorId == enfermera.SectorId);

            if (sector is null)
                throw new ExceptionNotFound("No se encontró el sector asociado a la enfermera.");

            return _mapeador.Mapear(sector);
        }
    }
}