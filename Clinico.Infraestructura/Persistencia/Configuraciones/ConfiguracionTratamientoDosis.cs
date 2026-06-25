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

                // --- ATRASADOS DE AYER (23 de Junio) ---
                new TratamientoDosis
                {
                    Id = Guid.Parse("77777777-1111-8888-8888-777777777777"),
                    TratamientoId = Guid.Parse("55555555-7777-7777-7777-555555555555"),
                    EnfermeraId = null,
                    FechaProgramada = new DateTime(2026, 06, 23, 8, 0, 0), // Ayer 08:00 AM (Atrasada)
                    Estado = (EstadoDosis)1 // Pendiente
                },
                new TratamientoDosis
                {
                    Id = Guid.Parse("77777777-2222-8888-8888-777777777777"),
                    TratamientoId = Guid.Parse("55555555-7777-7777-7777-555555555555"),
                    EnfermeraId = null,
                    FechaProgramada = new DateTime(2026, 06, 23, 16, 0, 0), // Ayer 16:00 PM (Atrasada)
                    Estado = (EstadoDosis)1 // Pendiente
                },

                // --- DE HOY (24 de Junio) ---
                new TratamientoDosis
                {
                    Id = Guid.Parse("77777777-3333-8888-8888-777777777777"),
                    TratamientoId = Guid.Parse("55555555-7777-7777-7777-555555555555"),
                    // ¡Esta simulamos que Rodrigo ya la dio hoy temprano!
                    EnfermeraId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    FechaProgramada = new DateTime(2026, 06, 24, 8, 0, 0), // Hoy 08:00 AM
                    FechaSuministro = new DateTime(2026, 06, 24, 8, 15, 0), // Se dio 15 mins tarde
                    FechaDelSistema = new DateTime(2026, 06, 24, 8, 15, 30),
                    Estado = (EstadoDosis)2 // Administrada
                },
                new TratamientoDosis
                {
                    Id = Guid.Parse("77777777-4444-8888-8888-777777777777"),
                    TratamientoId = Guid.Parse("55555555-7777-7777-7777-555555555555"),
                    EnfermeraId = null,
                    FechaProgramada = new DateTime(2026, 06, 24, 16, 0, 0), // Hoy 16:00 PM (Atrasada de hoy)
                    Estado = (EstadoDosis)1 // Pendiente
                },

                // --- HACIA EL FUTURO (25 y 26 de Junio) ---
                new TratamientoDosis
                {
                    Id = Guid.Parse("77777777-5555-8888-8888-777777777777"),
                    TratamientoId = Guid.Parse("55555555-7777-7777-7777-555555555555"),
                    EnfermeraId = null,
                    FechaProgramada = new DateTime(2026, 06, 25, 0, 0, 0), // Mañana a la medianoche
                    Estado = (EstadoDosis)1 // Pendiente
                },
                new TratamientoDosis
                {
                    Id = Guid.Parse("77777777-6666-8888-8888-777777777777"),
                    TratamientoId = Guid.Parse("55555555-7777-7777-7777-555555555555"),
                    EnfermeraId = null,
                    FechaProgramada = new DateTime(2026, 06, 25, 8, 0, 0), // Mañana 08:00 AM
                    Estado = (EstadoDosis)1 // Pendiente
                },
                new TratamientoDosis
                {
                    Id = Guid.Parse("77777777-7777-8888-8888-777777777777"),
                    TratamientoId = Guid.Parse("55555555-7777-7777-7777-555555555555"),
                    EnfermeraId = null,
                    FechaProgramada = new DateTime(2026, 06, 26, 8, 0, 0), // Pasado mañana 08:00 AM
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
                    FechaProgramada = new DateTime(2026, 06, 24, 10, 0, 0), // Hoy 10:00 AM
                    Estado = (EstadoDosis)1 // Pendiente
                },
                new TratamientoDosis
                {
                    Id = Guid.Parse("88888888-2222-8888-8888-888888888888"),
                    TratamientoId = Guid.Parse("66666666-7777-7777-7777-666666666666"),
                    EnfermeraId = null,
                    FechaProgramada = new DateTime(2026, 06, 24, 22, 0, 0), // Hoy 22:00 PM
                    Estado = (EstadoDosis)1 // Pendiente
                },
                new TratamientoDosis
                {
                    Id = Guid.Parse("88888888-3333-8888-8888-888888888888"),
                    TratamientoId = Guid.Parse("66666666-7777-7777-7777-666666666666"),
                    EnfermeraId = null,
                    FechaProgramada = new DateTime(2026, 06, 25, 10, 0, 0), // Mañana 10:00 AM
                    Estado = (EstadoDosis)1 // Pendiente
                }
            );
        }
    }
}