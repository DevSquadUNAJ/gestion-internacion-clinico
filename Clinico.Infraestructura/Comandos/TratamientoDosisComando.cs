using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Comandos;

public class TratamientoDosisComando : ITratamientoDosisComando
{
    private readonly ContextoBaseDeDatos _context;

    public TratamientoDosisComando(ContextoBaseDeDatos context)
    {
        _context = context;
    }

    public async Task ActualizarAsync(List<TratamientoDosis> dosis)
    {
        _context.TratamientosDosis.UpdateRange(dosis);
        await _context.SaveChangesAsync();
    }

    public async Task AgregarRangoAsync(List<TratamientoDosis> dosis)
    {
        await _context.TratamientosDosis.AddRangeAsync(dosis);
        await _context.SaveChangesAsync();
    }
}