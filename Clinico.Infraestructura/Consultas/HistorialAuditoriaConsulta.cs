using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas
{
    public class HistorialAuditoriaConsulta : IHistorialAuditoriaConsulta
    {
        private readonly ContextoBaseDeDatos _contexto;

        public HistorialAuditoriaConsulta(
            ContextoBaseDeDatos contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Auditoria>> ObtenerAuditoriasAsync(FiltroAuditoriaSolicitud filtros)
        {
            var consulta = _contexto.Auditorias.AsNoTracking();

            if (!string.IsNullOrEmpty(filtros.UsuarioId))
                consulta = consulta.Where(x => x.UsuarioId == filtros.UsuarioId);

            if (!string.IsNullOrEmpty(filtros.Rol))
                consulta = consulta.Where(x => x.Rol == filtros.Rol);

            if (!string.IsNullOrEmpty(filtros.Entidad))
                consulta = consulta.Where(x => x.Entidad == filtros.Entidad);

            if (filtros.EntidadId.HasValue)
                consulta = consulta.Where(x => x.EntidadId == filtros.EntidadId.Value);

            if (filtros.FechaDesde.HasValue)
                consulta = consulta.Where(x => x.FechaHora >= filtros.FechaDesde.Value);

            if (filtros.FechaHasta.HasValue)
                consulta = consulta.Where(x => x.FechaHora <= filtros.FechaHasta.Value);

            return await consulta.OrderByDescending(x => x.FechaHora).ToListAsync();
        }
    }
}
