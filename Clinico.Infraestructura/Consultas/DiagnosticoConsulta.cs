using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas;

public class DiagnosticoConsulta : IDiagnosticoConsulta
{
    private readonly ContextoBaseDeDatos _contexto;

    public DiagnosticoConsulta(ContextoBaseDeDatos contexto)
    {
        _contexto = contexto;
    }

    public async Task<Diagnostico?> ObtenerPorIdAsync(Guid diagnosticoId)
    {
        return await _contexto.Diagnosticos
            .Include(d => d.HistoriaClinica)
            .Include(d => d.CatalogoCie10)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == diagnosticoId);
    }
}