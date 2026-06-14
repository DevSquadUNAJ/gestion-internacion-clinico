using Clinico.Aplicacion.DTOs.Solicitudes;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            // Falta implementar filtros
            var consulta = _contexto.Auditorias.AsNoTracking();
            return await consulta.ToListAsync();
        }
    }
}
