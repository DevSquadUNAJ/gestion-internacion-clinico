using System;

namespace Clinico.Aplicacion.Excepciones
{
    public class ExceptionUnauthorized : Exception
    {
        public ExceptionUnauthorized(string message) : base(message)
        {
        }
    }
}