using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Excepciones
{
    public class EntidadNoEncontradaException : Exception
    {
        public EntidadNoEncontradaException(string mensaje): base(mensaje)
        {
        }
    }
}
