using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas;

public class CatalogoCie10Consulta: ICatalogoCie10Consulta
{
    private readonly ContextoBaseDeDatos _contexto;

    public CatalogoCie10Consulta(ContextoBaseDeDatos contexto)
    {
        _contexto = contexto;
    }

    public async Task<CatalogoCie10?> ObtenerPorCodigoAsync(string codigo)
    {
        return await _contexto.CatalogosCie10.AsNoTracking().FirstOrDefaultAsync(c => c.Codigo == codigo);
    }
}