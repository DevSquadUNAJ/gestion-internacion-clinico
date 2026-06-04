using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Dominio.Entidades
{
    public class Enfermera : EntityBase
    {
        public Guid SectorId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Legajo { get; set; } = string.Empty;

        public ICollection<TratamientoDosis> TratamientosDosis { get; set; } = new List<TratamientoDosis>();
    }
}
