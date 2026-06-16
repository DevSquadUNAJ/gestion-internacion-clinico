using Clinico.Dominio.Constantes;
using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionTratamiento : IEntityTypeConfiguration<Tratamiento>
    {
        public void Configure(EntityTypeBuilder<Tratamiento> builder)
        {
            builder.ToTable("Tratamientos");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Dosis)
                .HasPrecision(18, 2);

            builder.Property(t => t.Estado)
                .HasConversion<int>();

            builder.HasOne(t => t.Diagnostico)
                .WithMany(d => d.Tratamientos)
                .HasForeignKey(t => t.DiagnosticoId);

            builder.HasOne(t => t.Medicamento)
                .WithMany(m => m.Tratamientos)
                .HasForeignKey(t => t.MedicamentoId);

            builder.HasOne(t => t.UnidadMedida)
                .WithMany(um => um.Tratamientos)
                .HasForeignKey(t => t.UnidadMedidaId);

            builder.HasOne(t => t.FrecuenciaAdministracion)
                .WithMany(fa => fa.Tratamientos)
                .HasForeignKey(t => t.FrecuenciaAdministracionId);

            builder.HasData(
                new Tratamiento
                {
                    Id = Guid.Parse("55555555-7777-7777-7777-555555555555"),
                    DiagnosticoId = Guid.Parse("11111111-dddd-dddd-dddd-111111111111"), // Diagnóstico: Sinusitis
                    MedicamentoId = Guid.Parse("cccccccc-3333-3333-3333-cccccccccccc"), // Amoxidal
                    Dosis = 500m, // La "m" es para indicar que es un tipo decimal en C#
                    UnidadMedidaId = Guid.Parse("11111111-5555-5555-5555-111111111111"), // mg
                    FrecuenciaAdministracionId = Guid.Parse("33333333-6666-6666-6666-333333333333"), // Cada 8 horas
                    Estado = (EstadoTratamiento)1 // Casteamos al valor 1 (Activo en Enum)
                },
                new Tratamiento
                {
                    Id = Guid.Parse("66666666-7777-7777-7777-666666666666"),
                    DiagnosticoId = Guid.Parse("22222222-dddd-dddd-dddd-222222222222"), // Diagnóstico: Hipertensión
                    MedicamentoId = Guid.Parse("dddddddd-4444-4444-4444-dddddddddddd"), // Ibuprofeno
                    Dosis = 600m, // La "m" es para indicar que es un tipo decimal en C#
                    UnidadMedidaId = Guid.Parse("11111111-5555-5555-5555-111111111111"), // mg
                    FrecuenciaAdministracionId = Guid.Parse("44444444-6666-6666-6666-444444444444"), // Cada 12 horas
                    Estado = (EstadoTratamiento)1 // Casteamos al valor 1 (Activo en Enum)
                }
            );
        }
    }
}