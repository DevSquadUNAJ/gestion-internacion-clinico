using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Excepciones
{
    public sealed class NurseNotFoundException : Exception
    {
        public NurseNotFoundException(Guid nurseId)
            : base($"Nurse '{nurseId}' was not found.")
        {
        }
    }
}
