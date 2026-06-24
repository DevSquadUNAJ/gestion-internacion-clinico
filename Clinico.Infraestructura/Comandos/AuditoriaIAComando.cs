using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Comandos;

public class AuditoriaIAComando : IAuditoriaIAComando
{
    private readonly ContextoBaseDeDatos _contexto;

    public AuditoriaIAComando(ContextoBaseDeDatos contexto)
    {
        _contexto = contexto;
    }

    public async Task AgregarAsync(AuditoriaIA auditoriaIA)
    {
        await _contexto.AuditoriasIA.AddAsync(auditoriaIA);
        await _contexto.SaveChangesAsync();
    }
}