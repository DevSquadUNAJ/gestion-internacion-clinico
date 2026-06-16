using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionDiagnostico : IEntityTypeConfiguration<Diagnostico>
    {
        public void Configure(EntityTypeBuilder<Diagnostico> builder)
        {
            builder.ToTable("Diagnosticos");

            builder.HasKey(d => d.Id);

            builder.HasOne(d => d.HistoriaClinica)
                .WithMany(h => h.Diagnosticos)
                .HasForeignKey(d => d.HistoriaClinicaId);

            builder.HasOne(d => d.Medico)
                .WithMany(m => m.Diagnosticos)
                .HasForeignKey(d => d.MedicoId);

            builder.HasOne(d => d.CatalogoCie10)
                .WithMany(c => c.Diagnosticos)
                .HasForeignKey(d => d.CodigoCie10);

            builder.Property(d => d.Observaciones)
                .HasMaxLength(4000);

            builder.HasData(
                new Diagnostico
                {
                    Id = Guid.Parse("11111111-dddd-dddd-dddd-111111111111"),
                    HistoriaClinicaId = Guid.Parse("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), // Pertenece al Paciente 1
                    MedicoId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // Diagnosticado por Dr. Salas
                    CodigoCie10 = "J01.9", // Sinusitis
                    Observaciones = "Paciente presenta cuadro de congestión y dolor facial agudo."
                },
                new Diagnostico
                {
                    Id = Guid.Parse("22222222-dddd-dddd-dddd-222222222222"),
                    HistoriaClinicaId = Guid.Parse("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), // Pertenece al Paciente 2
                    MedicoId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), // Diagnosticado por Dr. Rojas
                    CodigoCie10 = "I10", // Hipertensión
                    Observaciones = "Presión arterial elevada en múltiples tomas. Requiere inicio de tratamiento."
                }
            );
        }
    }
}