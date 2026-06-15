using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IConsultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso
{
    public class ObtenerSeguimientoTratamientoCasoDeUso: IObtenerSeguimientoTratamientoCasoDeUso
    {
        private readonly ITratamientoConsulta _tratamientoConsulta;
        private readonly ITratamientoDosisConsulta _tratamientoDosisConsulta;

        public ObtenerSeguimientoTratamientoCasoDeUso(ITratamientoConsulta tratamientoConsulta, ITratamientoDosisConsulta tratamientoDosisConsulta)
        {
            _tratamientoConsulta = tratamientoConsulta;
            _tratamientoDosisConsulta = tratamientoDosisConsulta;
        }

        public async Task<ObtenerSeguimientoTratamientoRespuesta> EjecutarAsync(Guid tratamientoId)
        {
            var tratamiento =
                await _tratamientoConsulta.ObtenerPorIdAsync(tratamientoId);

            if (tratamiento is null)
                throw new ExceptionNotFound("No se encontró el tratamiento.");

            var dosis = await _tratamientoDosisConsulta.ObtenerPorTratamientoAsync(tratamientoId);

            return new ObtenerSeguimientoTratamientoRespuesta
            {
                TratamientoId = tratamiento.Id,

                Dosis = dosis.Select(d => new DosisSeguimientoRespuesta
                {
                    FechaProgramada = d.FechaProgramada,
                    FechaSuministro = d.FechaSuministro,
                    Estado = d.Estado,
                    Observaciones = d.Observaciones
                }).ToList()
            };
        }
    }
}
