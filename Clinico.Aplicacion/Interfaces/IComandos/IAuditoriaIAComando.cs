using Clinico.Dominio.Entidades;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IComandos
{
    public interface IAuditoriaIAComando
    {
        Task AgregarAsync(AuditoriaIA auditoriaIA);
        Task ActualizarAsync(AuditoriaIA auditoriaIA);
    }
}