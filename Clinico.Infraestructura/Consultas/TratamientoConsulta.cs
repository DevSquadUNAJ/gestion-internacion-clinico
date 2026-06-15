using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas;

public class TratamientoConsulta : ITratamientoConsulta
{
    private readonly ContextoBaseDeDatos _context;

    public TratamientoConsulta(ContextoBaseDeDatos context)
    {
        _context = context;
    }

    public async Task<Tratamiento?> ObtenerPorIdAsync(Guid id)
    {
        return await _context.Tratamientos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}