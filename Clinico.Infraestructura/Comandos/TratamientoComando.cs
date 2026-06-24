using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Comandos;

public class TratamientoComando : ITratamientoComando
{
    private readonly ContextoBaseDeDatos _context;

    public TratamientoComando(ContextoBaseDeDatos context)
    {
        _context = context;
    }

    public async Task ActualizarAsync(Tratamiento tratamiento)
    {
        _context.Tratamientos.Update(tratamiento);
        await _context.SaveChangesAsync();
    }
    public async Task AgregarAsync(Tratamiento tratamiento)
    {
        await _context.Tratamientos.AddAsync(tratamiento);
        await _context.SaveChangesAsync();
    }
}