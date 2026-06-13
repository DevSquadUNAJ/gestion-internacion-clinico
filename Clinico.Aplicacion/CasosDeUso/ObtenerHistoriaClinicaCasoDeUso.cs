using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Aplicacion.Interfaces.IMapeadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso
{
    public class ObtenerHistoriaClinicaCasoDeUso
    : IObtenerHistoriaClinicaCasoDeUso
    {
        private readonly IHistoriaClinicaConsulta _consulta;
        private readonly IHistoriaClinicaMapper _mapper;

        public ObtenerHistoriaClinicaCasoDeUso(IHistoriaClinicaConsulta consulta, IHistoriaClinicaMapper mapper)
        {
            _consulta = consulta;
            _mapper = mapper;
        }

        public async Task<ObtenerHistoriaClinicaRespuesta> EjecutarAsync(Guid pacienteId)
        {
            var historiaClinica =await _consulta.ObtenerPorPacienteIdAsync(pacienteId);

            if (historiaClinica is null)
            {
                throw new EntidadNoEncontradaException("Historia Clinica no encontrada");
            }

            return _mapper.Mapear(historiaClinica);
        }
    }
}
