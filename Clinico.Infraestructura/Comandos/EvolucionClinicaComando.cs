using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Comandos;

public class EvolucionClinicaComando : IEvolucionClinicaComando
{
    private readonly ContextoBaseDeDatos _contexto;

    public EvolucionClinicaComando(ContextoBaseDeDatos contexto)
    {
        _contexto = contexto;
    }

    public async Task AgregarAsync(EvolucionClinica evolucionClinica)
    {
        await _contexto.EvolucionesClinicas.AddAsync(evolucionClinica);
        await _contexto.SaveChangesAsync();
    }
}