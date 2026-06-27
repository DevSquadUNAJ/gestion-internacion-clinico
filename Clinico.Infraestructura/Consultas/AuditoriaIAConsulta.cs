using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas;

public class AuditoriaIAConsulta : IAuditoriaIAConsulta
{
    private readonly ContextoBaseDeDatos _contexto;

    public AuditoriaIAConsulta(ContextoBaseDeDatos contexto)
    {
        _contexto = contexto;
    }

    public async Task<AuditoriaIA?> ObtenerUltimaPorTratamientoParaActualizarAsync(Guid tratamientoId)
    {
        return await _contexto.AuditoriasIA
            .Where(a => a.TratamientoId == tratamientoId)
            .OrderByDescending(a => a.FechaHora)
            .FirstOrDefaultAsync();
    }
}