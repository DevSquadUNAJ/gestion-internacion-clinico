using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Excepciones
{
    public sealed class EnfermeraNotFoundException : Exception
    {
        public EnfermeraNotFoundException(Guid nurseId)
            : base($"Nurse '{nurseId}' was not found.")
        {
        }
    }
}
