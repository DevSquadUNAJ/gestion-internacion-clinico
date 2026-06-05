using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class TratamientoDosisConfiguration
        : IEntityTypeConfiguration<TratamientoDosis>
    {
        public void Configure(EntityTypeBuilder<TratamientoDosis> builder)
        {
            builder.ToTable("TratamientosDosis");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Estado)
                .HasConversion<int>();

            builder.HasOne(x => x.Tratamiento)
                .WithMany(x => x.DosisProgramadas)
                .HasForeignKey(x => x.TratamientoId);

            builder.HasOne(x => x.Enfermera)
                .WithMany(x => x.TratamientosDosis)
                .HasForeignKey(x => x.EnfermeraId);
        }
    }
}
