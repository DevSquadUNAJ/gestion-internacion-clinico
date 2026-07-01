using Clinico.Dominio.Base;
using System;
using System.Collections.Generic;

namespace Clinico.Dominio.Entidades
{
    public class Enfermera : EntidadBase
    {
        public Guid SectorId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Legajo { get; set; } = string.Empty;

        public ICollection<TratamientoDosis> TratamientosDosis { get; set; } = new List<TratamientoDosis>();
    }
}
