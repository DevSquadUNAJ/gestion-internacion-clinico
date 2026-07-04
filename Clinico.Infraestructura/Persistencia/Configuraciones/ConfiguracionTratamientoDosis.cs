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
                // ====================================================================
                // TRATAMIENTO 1: Sinusitis (Id: 555555...555) - Cada 8 horas
                // ====================================================================

                new TratamientoDosis
                {
                    Id = Guid.Parse("77777777-1111-8888-8888-777777777777"),
                    TratamientoId = Guid.Parse("55555555-7777-7777-7777-555555555555"),
                    EnfermeraId = null,
                    FechaProgramada = new DateTime(2026, 07, 06, 20, 0, 0), // Hoy 17:00 (Atrasada) (le resta 3 el front)
                    Estado = (EstadoDosis)1 // Pendiente
                },
                new TratamientoDosis
                {
                    Id = Guid.Parse("77777777-2222-8888-8888-777777777777"),
                    TratamientoId = Guid.Parse("55555555-7777-7777-7777-555555555555"),
                    EnfermeraId = null,
                    FechaProgramada = new DateTime(2026, 07, 07, 4, 0, 0), // Mañana 1:00 (le resta 3 el front)
                    Estado = (EstadoDosis)1 // Pendiente
                },

                // ====================================================================
                // TRATAMIENTO 2: Hipertensión (Id: 666666...666) - Cada 12 horas
                // ====================================================================
                new TratamientoDosis
                {
                    Id = Guid.Parse("88888888-1111-8888-8888-888888888888"),
                    TratamientoId = Guid.Parse("66666666-7777-7777-7777-666666666666"),
                    EnfermeraId = null,
                    FechaProgramada = new DateTime(2026, 07, 06, 21, 0, 0), // Hoy 18:00 (Atrasada) (le resta 3 el front)
                    Estado = (EstadoDosis)1 // Pendiente
                },
                new TratamientoDosis
                {
                    Id = Guid.Parse("88888888-2222-8888-8888-888888888888"),
                    TratamientoId = Guid.Parse("66666666-7777-7777-7777-666666666666"),
                    EnfermeraId = null,
                    FechaProgramada = new DateTime(2026, 07, 07, 09, 0, 0), // Mañana 06:00 (le resta 3 el front)
                    Estado = (EstadoDosis)1 // Pendiente
                }
            );
        }
    }
}