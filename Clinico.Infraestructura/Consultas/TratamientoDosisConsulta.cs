using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Constantes;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas;

public class TratamientoDosisConsulta : ITratamientoDosisConsulta
{
    private readonly ContextoBaseDeDatos _context;

    public TratamientoDosisConsulta(ContextoBaseDeDatos context)
    {
        _context = context;
    }

    public async Task<List<TratamientoDosis>> ObtenerPorTratamientoAsync(Guid tratamientoId)
    {
        return await _context.TratamientosDosis.Where(td => td.TratamientoId == tratamientoId).ToListAsync();
    }

    public async Task<IEnumerable<TratamientoDosis>> ObtenerPendientesPorPacientesAsync(IEnumerable<Guid> pacientesIds)
    {
        return await _context.TratamientosDosis
            .Include(td => td.Tratamiento)
                .ThenInclude(t => t.Medicamento)
            .Include(td => td.Tratamiento)
                .ThenInclude(t => t.Diagnostico)
                    .ThenInclude(d => d.HistoriaClinica)
            .Where(td => td.Estado == EstadoDosis.Pendiente
                      && pacientesIds.Contains(td.Tratamiento.Diagnostico.HistoriaClinica.PacienteId))
            .AsNoTracking()
            .ToListAsync();
    }
}