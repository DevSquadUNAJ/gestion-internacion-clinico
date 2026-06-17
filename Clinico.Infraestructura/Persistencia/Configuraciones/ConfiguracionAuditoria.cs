using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionAuditoria : IEntityTypeConfiguration<Auditoria>
    {
        public void Configure(EntityTypeBuilder<Auditoria> builder)
        {
            builder.ToTable("Auditorias");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.UsuarioId)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.Rol)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.Accion)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Entidad)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.EntidadId)
                .IsRequired();

            builder.Property(a => a.Descripcion)
                .HasMaxLength(500);

            // PayloadJson no lleva MaxLength para permitir que actúe como nvarchar(max) en SQL Server
            builder.Property(a => a.PayloadJson);

            builder.Property(a => a.FechaHora)
                .IsRequired();

            builder.HasData(
                new Auditoria
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    UsuarioId = "22222222-2222-2222-2222-222222222222", // El ID del usuario en el microservicio Seguridad
                    Rol = "Medico",
                    Accion = "Crear",
                    Entidad = "Diagnostico",
                    EntidadId = Guid.Parse("11111111-dddd-dddd-dddd-111111111111"), // El Guid del diagnóstico de Sinusitis
                    Descripcion = "Se registró un nuevo diagnóstico de Sinusitis aguda.",
                    PayloadJson = "{\"CodigoCie10\": \"J01.9\"}",
                    FechaHora = new DateTime(2026, 6, 16, 10, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}