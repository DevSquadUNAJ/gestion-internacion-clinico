using Clinico.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IConsultas
{
    public interface ICatalogoCie10Consulta
    {
        Task<CatalogoCie10?> ObtenerPorCodigoAsync(string codigo);
    }
}
