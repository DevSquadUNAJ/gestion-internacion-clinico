using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionAuditoriaIA : IEntityTypeConfiguration<AuditoriaIA>
    {
        public void Configure(EntityTypeBuilder<AuditoriaIA> builder)
        {
            builder.ToTable("AuditoriasIA");

            builder.HasKey(aia => aia.Id);

            builder.HasOne(aia => aia.Tratamiento)
                   .WithMany(t => t.AuditoriasIA)
                   .HasForeignKey(aia => aia.TratamientoId);

            builder.Property(aia => aia.MensajeIA)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(aia => aia.JustificacionClinica)
                .HasColumnType("nvarchar(max)")
                .HasMaxLength(1000);

            builder.Property(aia => aia.FechaHora)
                .IsRequired();

            builder.Property(aia => aia.NivelRiesgo)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.HasData(
                new AuditoriaIA
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    TratamientoId = Guid.Parse("66666666-7777-7777-7777-666666666666"), // Tratamiento con Ibuprofeno
                    NivelRiesgo = Dominio.Constantes.NivelRiesgoIA.Medio,
                    AlertaDetectada = true,
                    MensajeIA = "⚠️ Precaución: El uso de AINEs (Ibuprofeno) puede aumentar la presión arterial en pacientes hipertensos.",
                    FueForzado = true,
                    JustificacionClinica = "El paciente presenta dolor agudo inmanejable. Se administrará dosis baja y se monitoreará la presión arterial cada 8 horas.",
                    FechaHora = new DateTime(2026, 6, 16, 10, 30, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}