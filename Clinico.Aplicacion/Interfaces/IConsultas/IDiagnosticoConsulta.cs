using Clinico.Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IConsultas
{
    public interface IDiagnosticoConsulta
    {
        Task<Diagnostico?> ObtenerPorIdAsync(Guid diagnosticoId);
    }
}