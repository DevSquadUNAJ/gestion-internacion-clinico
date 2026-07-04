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
            DateTime? fechaHoraDesde, // <--- NUEVO PARÁMETRO
            DateTime? fechaHoraHasta, // <--- NUEVO PARÁMETRO
            CancellationToken cancellationToken)
        {
            // 1. Armamos la consulta base con los Includes necesarios
            var consulta = _contexto.TratamientosDosis
                .Include(d => d.Tratamiento).ThenInclude(t => t.Medicamento)
                .Include(d => d.Tratamiento).ThenInclude(t => t.UnidadMedida)
                .Include(d => d.Tratamiento).ThenInclude(t => t.Diagnostico).ThenInclude(diag => diag.HistoriaClinica)
                .Where(d => pacientesIds.Contains(d.Tratamiento.Diagnostico.HistoriaClinica.PacienteId));

            // 2. Lógica de filtrado de Fechas y Horas
            if (fechaHoraDesde.HasValue && fechaHoraHasta.HasValue)
            {
                // Si el Front nos manda el rango del turno de la enfermera, filtramos exacto
                consulta = consulta.Where(d =>
                    d.FechaProgramada >= fechaHoraDesde.Value &&
                    d.FechaProgramada <= fechaHoraHasta.Value);
            }
            else
            {
                // Fallback: Comportamiento original buscando todo lo del día
                var inicio = fecha.Date;
                var fin = inicio.AddDays(1);

                consulta = consulta.Where(d =>
                    d.FechaProgramada >= inicio &&
                    d.FechaProgramada < fin);
            }

            // 3. Filtrar por estados de la dosis si se envían (ej. Pendientes)
            if (estados is not null && estados.Count > 0)
            {
                consulta = consulta.Where(d => estados.Contains(d.Estado));
            }

            var totalRegistros = await consulta.CountAsync(cancellationToken);

            // 4. Aplicar paginación optimizada
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