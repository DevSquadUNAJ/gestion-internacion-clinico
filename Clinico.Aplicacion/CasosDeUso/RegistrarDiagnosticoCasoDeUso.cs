using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Excepciones;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Aplicacion.Interfaces.IExternos;
using Clinico.Aplicacion.Interfaces.IMapeadores;
using Clinico.Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.CasosDeUso
{
    public class RegistrarDiagnosticoCasoDeUso : IRegistrarDiagnosticoCasoDeUso
    {
        private readonly IHistoriaClinicaConsulta _historiaClinicaConsulta;
        private readonly IMedicoConsulta _medicoConsulta;
        private readonly ICatalogoCie10Consulta _catalogoCie10Consulta;
        private readonly IDiagnosticoComando _diagnosticoComando;
        private readonly IAdmisionServicio _admisionServicio;
        private readonly IRegistrarDiagnosticoMapeador _mapeador;

        public RegistrarDiagnosticoCasoDeUso(
            IHistoriaClinicaConsulta historiaClinicaConsulta,
            IMedicoConsulta medicoConsulta,
            ICatalogoCie10Consulta catalogoCie10Consulta,
            IDiagnosticoComando diagnosticoComando,
            IAdmisionServicio admisionServicio,
            IRegistrarDiagnosticoMapeador mapeador)
        {
            _historiaClinicaConsulta = historiaClinicaConsulta;
            _medicoConsulta = medicoConsulta;
            _catalogoCie10Consulta = catalogoCie10Consulta;
            _diagnosticoComando = diagnosticoComando;
            _admisionServicio = admisionServicio;
            _mapeador = mapeador;
        }

        public async Task<RegistrarDiagnosticoRespuesta> EjecutarAsync(RegistrarDiagnosticoSolicitud solicitud)
        {
            var contextoInternacion = await _admisionServicio.ObtenerContextoInternacionAsync(solicitud.InternacionId);
            var pacienteId = contextoInternacion.PacienteId;

            var historiaClinica = await _historiaClinicaConsulta.ObtenerPorPacienteIdAsync(pacienteId);
            if (historiaClinica is null)
                throw new ExceptionNotFound("El paciente no tiene una historia clínica registrada.");

            var medico = await _medicoConsulta.ObtenerPorIdAsync(solicitud.MedicoId);
            if (medico is null)
                throw new ExceptionNotFound("El médico indicado no existe.");

            var codigoCie10 = await _catalogoCie10Consulta.ObtenerPorCodigoAsync(solicitud.CodigoCie10);
            if (codigoCie10 is null)
                throw new ExceptionNotFound("El código CIE-10 indicado no existe.");

            var diagnostico = new Diagnostico
            {
                Id = Guid.NewGuid(),
                HistoriaClinicaId = historiaClinica.Id,
                MedicoId = medico.Id,
                CodigoCie10 = codigoCie10.Codigo,
                FechaHora = DateTime.UtcNow,
                Observaciones = solicitud.Observaciones
            };

            await _diagnosticoComando.AgregarAsync(diagnostico);

            return _mapeador.Mapear(diagnostico);
        }
    }
}