using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class DiagnosticoConfiguration
        : IEntityTypeConfiguration<Diagnostico>
    {
        public void Configure(EntityTypeBuilder<Diagnostico> builder)
        {
            builder.ToTable("Diagnosticos");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.HistoriaClinica)
                .WithMany(x => x.Diagnosticos)
                .HasForeignKey(x => x.HistoriaClinicaId);

            builder.HasOne(x => x.Medico)
                .WithMany(x => x.Diagnosticos)
                .HasForeignKey(x => x.MedicoId);

            builder.HasOne(x => x.CatalogoCie10)
                .WithMany(x => x.Diagnosticos)
                .HasForeignKey(x => x.CodigoCie10);

            builder.Property(x => x.Observaciones)
                .HasMaxLength(4000);
        }
    }
}
