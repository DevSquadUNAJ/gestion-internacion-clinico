using Clinico.Dominio.Constantes;
using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionTratamientoDosis : IEntityTypeConfiguration<TratamientoDosis>
    {
        public void Configure(EntityTypeBuilder<TratamientoDosis> builder)
        {
            builder.ToTable("TratamientosDosis");

            builder.HasKey(td => td.Id);

            builder.Property(td => td.Estado)
                .HasConversion<int>();

            builder.HasOne(td => td.Tratamiento)
                .WithMany(t => t.DosisProgramadas)
                .HasForeignKey(td => td.TratamientoId);

            builder.HasOne(td => td.Enfermera)
                .WithMany(e => e.TratamientosDosis)
                .HasForeignKey(td => td.EnfermeraId);

            builder.HasData(
                new TratamientoDosis
                {
                    Id = Guid.Parse("77777777-8888-8888-8888-777777777777"),
                    TratamientoId = Guid.Parse("55555555-7777-7777-7777-555555555555"), // Tratamiento de Sinusitis
                    EnfermeraId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), // Enf. Rodrigo Godoy
                    Estado = (EstadoDosis)1 // "Pendiente" en Enum
                },
                new TratamientoDosis
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    TratamientoId = Guid.Parse("55555555-7777-7777-7777-555555555555"), // Siguiente dosis de Sinusitis
                    EnfermeraId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), // Enf. Matías Silva
                    Estado = (EstadoDosis)1 // "Pendiente" en Enum
                }
            );
        }
    }
}