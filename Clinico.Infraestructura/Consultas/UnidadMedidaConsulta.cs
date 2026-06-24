using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas;

public class UnidadMedidaConsulta : IUnidadMedidaConsulta
{
    private readonly ContextoBaseDeDatos _contexto;

    public UnidadMedidaConsulta(ContextoBaseDeDatos contexto)
    {
        _contexto = contexto;
    }

    public async Task<UnidadMedida?> ObtenerPorIdAsync(Guid id)
    {
        return await _contexto.UnidadesMedida
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}