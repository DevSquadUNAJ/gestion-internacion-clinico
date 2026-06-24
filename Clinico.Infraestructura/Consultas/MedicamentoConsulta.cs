using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas;

public class MedicamentoConsulta : IMedicamentoConsulta
{
    private readonly ContextoBaseDeDatos _contexto;

    public MedicamentoConsulta(ContextoBaseDeDatos contexto)
    {
        _contexto = contexto;
    }

    public async Task<Medicamento?> ObtenerPorIdAsync(Guid id)
    {
        return await _contexto.Medicamentos
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}