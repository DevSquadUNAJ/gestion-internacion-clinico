using Clinico.Dominio.Entidades;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IComandos
{
    public interface IAuditoriaComando
    {
        Task AgregarAsync(Auditoria auditoria);
    }
}