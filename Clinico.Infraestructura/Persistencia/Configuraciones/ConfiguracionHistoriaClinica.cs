using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionHistoriaClinica : IEntityTypeConfiguration<HistoriaClinica>
    {
        public void Configure(EntityTypeBuilder<HistoriaClinica> builder)
        {
            builder.ToTable("HistoriasClinicas");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.PacienteId)
                .IsRequired();

            builder.Property(h => h.GrupoSanguineo)
                .HasMaxLength(5);

            builder.Property(h => h.Alergias)
                .HasMaxLength(2000);

            builder.Property(h => h.Antecedentes)
                .HasMaxLength(4000);

            builder.Property(h => h.ObservacionesGenerales)
                .HasMaxLength(4000);

            builder.HasData(
                new HistoriaClinica
                {
                    Id = Guid.Parse("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"),
                    PacienteId = Guid.Parse("11111111-aaaa-aaaa-aaaa-111111111111"), // Paciente 1 (Admisión)
                    GrupoSanguineo = "A+",
                    Alergias = "Penicilina",
                    Antecedentes = "Hipertensión controlada",
                    ObservacionesGenerales = "Paciente ingresa por guardia clínica."
                },
                new HistoriaClinica
                {
                    Id = Guid.Parse("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"),
                    PacienteId = Guid.Parse("22222222-bbbb-bbbb-bbbb-222222222222"), // Paciente 2 (Admisión)
                    GrupoSanguineo = "O-",
                    Alergias = "Ninguna conocida",
                    Antecedentes = "Asma en la infancia",
                    ObservacionesGenerales = "Paciente derivado de consultorios externos."
                }
            );
        }
    }
}