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
                    PacienteId = Guid.Parse("11111111-aaaa-aaaa-aaaa-111111111111"), // Carlos Mendoza
                    GrupoSanguineo = "A+",
                    Alergias = "Penicilina",
                    Antecedentes = "Hipertensión controlada",
                    ObservacionesGenerales = "Paciente ingresa por guardia clínica."
                },
                new HistoriaClinica
                {
                    Id = Guid.Parse("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"),
                    PacienteId = Guid.Parse("22222222-bbbb-bbbb-bbbb-222222222222"), // Luciana Gómez
                    GrupoSanguineo = "O-",
                    Alergias = "Ninguna conocida",
                    Antecedentes = "Asma en la infancia",
                    ObservacionesGenerales = "Paciente derivado de consultorios externos."
                },
                new HistoriaClinica
                {
                    Id = Guid.Parse("cccccccc-3333-3333-3333-cccccccccccc"),
                    PacienteId = Guid.Parse("33333333-2373-cccc-cccc-333333333333"), // Roberto Sánchez
                    GrupoSanguineo = "B+",
                    Alergias = "Ibuprofeno",
                    Antecedentes = "Cirugía de apéndice en 2015",
                    ObservacionesGenerales = "Control de rutina."
                },
                new HistoriaClinica
                {
                    Id = Guid.Parse("dddddddd-4444-4444-4444-dddddddddddd"),
                    PacienteId = Guid.Parse("44444444-2474-dddd-dddd-444444444444"), // María Fernández
                    GrupoSanguineo = "AB+",
                    Alergias = "Ninguna conocida",
                    Antecedentes = "Diabetes Tipo 2",
                    ObservacionesGenerales = "Requiere monitoreo de glucosa."
                },
                new HistoriaClinica
                {
                    Id = Guid.Parse("eeeeeeee-5555-5555-5555-eeeeeeeeeeee"),
                    PacienteId = Guid.Parse("55555555-2575-eeee-eeee-555555555555"), // Javier Rodríguez
                    GrupoSanguineo = "O+",
                    Alergias = "Lactosa, Maní",
                    Antecedentes = "Sin antecedentes clínicos de relevancia",
                    ObservacionesGenerales = "Chequeo pre-ocupacional."
                },
                new HistoriaClinica
                {
                    Id = Guid.Parse("ffffffff-6666-6666-6666-ffffffffffff"),
                    PacienteId = Guid.Parse("66666666-2676-ffff-ffff-666666666666"), // Silvana López
                    GrupoSanguineo = "A-",
                    Alergias = "Ninguna conocida",
                    Antecedentes = "Hipotiroidismo",
                    ObservacionesGenerales = "Medicación diaria con Levotiroxina."
                },
                new HistoriaClinica
                {
                    Id = Guid.Parse("aaaaaaaa-7777-7777-7777-aaaaaaaaaaaa"),
                    PacienteId = Guid.Parse("77777777-2777-aaaa-aaaa-777777777777"), // Diego Martínez
                    GrupoSanguineo = "B-",
                    Alergias = "Polvo, Ácaros",
                    Antecedentes = "Fractura de fémur en 2010",
                    ObservacionesGenerales = "Fisioterapia ocasional por dolor articular."
                },
                new HistoriaClinica
                {
                    Id = Guid.Parse("bbbbbbbb-8888-8888-8888-bbbbbbbbbbbb"),
                    PacienteId = Guid.Parse("88888888-2878-bbbb-bbbb-888888888888"), // Valeria Torres
                    GrupoSanguineo = "O+",
                    Alergias = "Amoxicilina",
                    Antecedentes = "Migrañas crónicas",
                    ObservacionesGenerales = "Tratamiento neurológico en curso."
                },
                new HistoriaClinica
                {
                    Id = Guid.Parse("cccccccc-9999-9999-9999-cccccccccccc"),
                    PacienteId = Guid.Parse("99999999-2979-cccc-cccc-999999999999"), // Gustavo Romero
                    GrupoSanguineo = "AB-",
                    Alergias = "Ninguna conocida",
                    Antecedentes = "Colesterol alto",
                    ObservacionesGenerales = "Dieta estricta y control cardiológico."
                },
                new HistoriaClinica
                {
                    Id = Guid.Parse("dddddddd-1010-1010-1010-dddddddddddd"),
                    PacienteId = Guid.Parse("10101010-3080-dddd-dddd-101010101010"), // Natalia Silva
                    GrupoSanguineo = "A+",
                    Alergias = "Picadura de abejas",
                    Antecedentes = "Episodio de anafilaxia en 2022",
                    ObservacionesGenerales = "Porta autoinyector de epinefrina."
                }
            );
        }
    }
}