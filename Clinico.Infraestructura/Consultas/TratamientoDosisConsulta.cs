using Clinico.Aplicacion.Interfaces.IConsultas;
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
}