using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas;

public class MedicoConsulta : IMedicoConsulta
{
    private readonly ContextoBaseDeDatos _contexto;

    public MedicoConsulta(ContextoBaseDeDatos contexto)
    {
        _contexto = contexto;
    }

    public async Task<Medico?> ObtenerPorIdAsync(Guid id)
    {
        return await _contexto.Medicos.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
    }
}