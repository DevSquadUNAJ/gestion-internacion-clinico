using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas;

public class FrecuenciaAdministracionConsulta : IFrecuenciaAdministracionConsulta
{
    private readonly ContextoBaseDeDatos _context;

    public FrecuenciaAdministracionConsulta(ContextoBaseDeDatos context)
    {
        _context = context;
    }

    public async Task<FrecuenciaAdministracion?> ObtenerPorIdAsync(Guid id)
    {
        return await _context.FrecuenciasAdministracion.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
    }
}