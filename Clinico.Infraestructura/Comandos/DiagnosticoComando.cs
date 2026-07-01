using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Comandos;

public class DiagnosticoComando: IDiagnosticoComando
{
    private readonly ContextoBaseDeDatos _contexto;

    public DiagnosticoComando(ContextoBaseDeDatos contexto)
    {
        _contexto = contexto;
    }

    public async Task AgregarAsync(Diagnostico diagnostico)
    {
        await _contexto.Diagnosticos.AddAsync(diagnostico);
        await _contexto.SaveChangesAsync();
    }
}