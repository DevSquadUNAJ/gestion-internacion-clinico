using System;
using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionMedico : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("Medicos");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Nombre)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(m => m.Matricula)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(m => m.Matricula)
                .IsUnique();

            builder.Property(m => m.Especialidad)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasData(
                new Medico
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Nombre = "Dr. Alejandro Salas",
                    Matricula = "MN-123456",
                    Especialidad = "Clínica Médica"
                },
                new Medico
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    Nombre = "Dr. Yonatan Rojas",
                    Matricula = "MN-654321",
                    Especialidad = "Terapia Intensiva"
                }
            );
        }
    }
}