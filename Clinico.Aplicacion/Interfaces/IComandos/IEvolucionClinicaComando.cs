using Clinico.Dominio.Entidades;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IComandos;

public interface IEvolucionClinicaComando
{
    Task AgregarAsync(EvolucionClinica evolucionClinica);
}