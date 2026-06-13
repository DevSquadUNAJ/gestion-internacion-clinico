using Clinico.Aplicacion.DTOs;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas;

public sealed class GetNursingDashboardQuery
    : IGetNursingDashboardQuery
{
    private readonly ContextoBaseDeDatos _context;

    public GetNursingDashboardQuery(
        ContextoBaseDeDatos context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<NursingDashboardItemDto>>
        GetPendingBySectorAsync(
            Guid sectorId,
            CancellationToken cancellationToken)
    {
        return await _context.TratamientosDosis
            .Include(td => td.Tratamiento)
                .ThenInclude(t => t.Medicamento)
            .Include(td => td.Tratamiento)
                .ThenInclude(t => t.Diagnostico)
                    .ThenInclude(d => d.HistoriaClinica)
            .Where(td => td.Estado == EstadoDosis.Pendiente)
            .Select(td => new NursingDashboardItemDto(
                td.Id,
                td.Tratamiento.Diagnostico.HistoriaClinica.PacienteId.ToString(),
                td.Tratamiento.Medicamento.NombreComercial,
                td.FechaProgramada))
            .ToListAsync(cancellationToken);
    }
}