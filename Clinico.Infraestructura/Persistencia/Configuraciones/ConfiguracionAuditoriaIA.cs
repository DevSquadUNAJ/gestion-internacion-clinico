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

            // Asumimos que Tratamiento no tiene una colección explícita de AuditoriasIA en su clase,
            // así que el .WithMany() queda vacío, lo cual es perfectamente válido en EF Core.
            builder.HasOne(aia => aia.Tratamiento)
                .WithMany()
                .HasForeignKey(aia => aia.TratamientoId);

            builder.Property(aia => aia.MensajeIA)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(aia => aia.JustificacionClinica)
                .HasMaxLength(1000);

            builder.Property(aia => aia.FechaHora)
                .IsRequired();

            builder.HasData(
                new AuditoriaIA
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    TratamientoId = Guid.Parse("66666666-7777-7777-7777-666666666666"), // Tratamiento con Ibuprofeno
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