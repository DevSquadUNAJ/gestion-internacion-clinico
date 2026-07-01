using Clinico.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IComandos
{
    public interface ITratamientoDosisComando
    {
        Task ActualizarAsync(List<TratamientoDosis> dosis);
        Task AgregarRangoAsync(List<TratamientoDosis> dosis);
    }
}