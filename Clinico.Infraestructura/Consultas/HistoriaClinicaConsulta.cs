using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas
{
    public class HistoriaClinicaConsulta : IHistoriaClinicaConsulta
    {
        private readonly ContextoBaseDeDatos _contexto;

        public HistoriaClinicaConsulta(
            ContextoBaseDeDatos contexto)
        {
            _contexto = contexto;
        }

        public async Task<HistoriaClinica?> ObtenerPorPacienteIdAsync(Guid pacienteId)
        {
            return await _contexto.HistoriasClinicas
                        .Include(h => h.Diagnosticos).ThenInclude(d => d.CatalogoCie10)
                        .Include(h => h.Diagnosticos).ThenInclude(d => d.Tratamientos).ThenInclude(t => t.Medicamento)
                        .Include(h => h.Diagnosticos).ThenInclude(d => d.Tratamientos).ThenInclude(t => t.UnidadMedida)
                        .Include(h => h.Diagnosticos).ThenInclude(d => d.Tratamientos).ThenInclude(t => t.FrecuenciaAdministracion)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(h => h.PacienteId == pacienteId);
        }

        public async Task<HistoriaClinica?> ObtenerPorIdAsync(Guid id)
        {
            return await _contexto.HistoriasClinicas.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}
