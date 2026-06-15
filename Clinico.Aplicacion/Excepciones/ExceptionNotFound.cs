using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Excepciones
{
    public class ExceptionNotFound : Exception
    {
        public ExceptionNotFound(string mensaje): base(mensaje)
        {
        }
    }
}
