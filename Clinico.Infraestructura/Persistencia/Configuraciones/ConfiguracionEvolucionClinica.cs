using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionEvolucionClinica : IEntityTypeConfiguration<EvolucionClinica>
    {
        public void Configure(EntityTypeBuilder<EvolucionClinica> builder)
        {
            builder.ToTable("EvolucionesClinicas");

            builder.HasKey(ec => ec.Id);

            builder.HasOne(ec => ec.HistoriaClinica)
                .WithMany(h => h.EvolucionesClinicas)
                .HasForeignKey(ec => ec.HistoriaClinicaId);

            builder.HasOne(ec => ec.Medico)
                .WithMany(m => m.EvolucionesClinicas)
                .HasForeignKey(ec => ec.MedicoId);

            builder.Property(ec => ec.Observacion)
                .HasMaxLength(4000)
                .IsRequired();

            builder.Property(ec => ec.FechaHora)
                .IsRequired();

            builder.HasData(
                new EvolucionClinica
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    HistoriaClinicaId = Guid.Parse("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), // Historia Clínica 1
                    MedicoId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // Dr. Salas
                    Observacion = "Paciente refiere alivio leve del dolor facial tras la primera dosis de Amoxicilina. Afebril. Continúa en observación.",
                    FechaHora = new DateTime(2026, 6, 16, 18, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
