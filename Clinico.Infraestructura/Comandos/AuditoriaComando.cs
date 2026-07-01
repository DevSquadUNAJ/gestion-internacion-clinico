using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Comandos;

public class AuditoriaComando : IAuditoriaComando
{
    private readonly ContextoBaseDeDatos _contexto;

    public AuditoriaComando(ContextoBaseDeDatos contexto)
    {
        _contexto = contexto;
    }

    public async Task AgregarAsync(Auditoria auditoria)
    {
        await _contexto.Auditorias.AddAsync(auditoria);
        await _contexto.SaveChangesAsync();
    }
}