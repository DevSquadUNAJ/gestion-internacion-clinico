using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Constantes;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas
{
    public sealed class ObtenerDosisProgramadasConsulta : IObtenerDosisProgramadasConsulta
    {
        private readonly ContextoBaseDeDatos _contexto;

        public ObtenerDosisProgramadasConsulta(ContextoBaseDeDatos contexto)
        {
            _contexto = contexto;
        }

        public async Task<(List<TratamientoDosis> Elementos, int TotalRegistros)> ObtenerAsync(
            IReadOnlyCollection<Guid> pacientesIds,
            DateTime fecha,
            IReadOnlyCollection<EstadoDosis>? estados,
            int pagina,
            int tamPagina,
            CancellationToken cancellationToken)
        {
            var inicio = fecha.Date;
            var fin = inicio.AddDays(1);

            var consulta = _contexto.TratamientosDosis
                .Include(d => d.Tratamiento).ThenInclude(t => t.Medicamento)
                .Include(d => d.Tratamiento).ThenInclude(t => t.UnidadMedida)
                .Include(d => d.Tratamiento).ThenInclude(t => t.Diagnostico).ThenInclude(diag => diag.HistoriaClinica)
                .Where(d =>
                    d.FechaProgramada >= inicio &&
                    d.FechaProgramada < fin &&
                    pacientesIds.Contains(d.Tratamiento.Diagnostico.HistoriaClinica.PacienteId));

            if (estados is not null && estados.Count > 0)
            {
                consulta = consulta.Where(d => estados.Contains(d.Estado));
            }

            var totalRegistros = await consulta.CountAsync(cancellationToken);

            var elementos = await consulta
                .OrderBy(d => d.FechaProgramada)
                .Skip((pagina - 1) * tamPagina)
                .Take(tamPagina)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return (elementos, totalRegistros);
        }
    }
}