using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinico.Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auditorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Entidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntidadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PayloadJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogosCie10",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogosCie10", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Enfermeras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Legajo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enfermeras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FrecuenciasAdministracion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CantidadHoras = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrecuenciasAdministracion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoriasClinicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrupoSanguineo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Alergias = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Antecedentes = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    ObservacionesGenerales = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriasClinicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreComercial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DrogaGenerica = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Presentacion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Contraindicaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EfectosAdversos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViaAdministracion = table.Column<int>(type: "int", nullable: false),
                    RequiereControl = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Especialidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesMedida",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Abreviatura = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesMedida", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvolucionesClinicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HistoriaClinicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvolucionesClinicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvolucionesClinicas_HistoriasClinicas_HistoriaClinicaId",
                        column: x => x.HistoriaClinicaId,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosticos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HistoriaClinicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoCie10 = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosticos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnosticos_CatalogosCie10_CodigoCie10",
                        column: x => x.CodigoCie10,
                        principalTable: "CatalogosCie10",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diagnosticos_HistoriasClinicas_HistoriaClinicaId",
                        column: x => x.HistoriaClinicaId,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diagnosticos_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tratamientos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiagnosticoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnidadMedidaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FrecuenciaAdministracionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dosis = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tratamientos_Diagnosticos_DiagnosticoId",
                        column: x => x.DiagnosticoId,
                        principalTable: "Diagnosticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamientos_FrecuenciasAdministracion_FrecuenciaAdministracionId",
                        column: x => x.FrecuenciaAdministracionId,
                        principalTable: "FrecuenciasAdministracion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamientos_Medicamentos_MedicamentoId",
                        column: x => x.MedicamentoId,
                        principalTable: "Medicamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamientos_UnidadesMedida_UnidadMedidaId",
                        column: x => x.UnidadMedidaId,
                        principalTable: "UnidadesMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditoriasIA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TratamientoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NivelRiesgo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AlertaDetectada = table.Column<bool>(type: "bit", nullable: false),
                    MensajeIA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FueForzado = table.Column<bool>(type: "bit", nullable: false),
                    JustificacionClinica = table.Column<string>(type: "nvarchar(max)", maxLength: 1000, nullable: true),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditoriasIA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditoriasIA_Tratamientos_TratamientoId",
                        column: x => x.TratamientoId,
                        principalTable: "Tratamientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TratamientosDosis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TratamientoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnfermeraId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaProgramada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSuministro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaDelSistema = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    MotivoOmision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TratamientosDosis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TratamientosDosis_Enfermeras_EnfermeraId",
                        column: x => x.EnfermeraId,
                        principalTable: "Enfermeras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TratamientosDosis_Tratamientos_TratamientoId",
                        column: x => x.TratamientoId,
                        principalTable: "Tratamientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Auditorias",
                columns: new[] { "Id", "Accion", "Descripcion", "Entidad", "EntidadId", "FechaHora", "PayloadJson", "Rol", "UsuarioId" },
                values: new object[] { new Guid("99999999-9999-9999-9999-999999999999"), "Crear", "Se registró un nuevo diagnóstico de Sinusitis aguda.", "Diagnostico", new Guid("11111111-dddd-dddd-dddd-111111111111"), new DateTime(2026, 6, 16, 10, 0, 0, 0, DateTimeKind.Utc), "{\"CodigoCie10\": \"J01.9\"}", "Medico", "22222222-2222-2222-2222-222222222222" });

            migrationBuilder.InsertData(
                table: "CatalogosCie10",
                columns: new[] { "Codigo", "Categoria", "Descripcion" },
                values: new object[,]
                {
                    { "I10", "Enfermedades del sistema circulatorio", "Hipertensión esencial (primaria)" },
                    { "J01.9", "Enfermedades del sistema respiratorio", "Sinusitis aguda, no especificada" }
                });

            migrationBuilder.InsertData(
                table: "Enfermeras",
                columns: new[] { "Id", "Legajo", "Nombre", "SectorId" },
                values: new object[,]
                {
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "ENF-1001", "Enf. Rodrigo Godoy", new Guid("88888888-8888-8888-8888-888888888888") },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "ENF-1002", "Enf. Matías Silva", new Guid("99999999-9999-9999-9999-999999999999") }
                });

            migrationBuilder.InsertData(
                table: "FrecuenciasAdministracion",
                columns: new[] { "Id", "CantidadHoras", "Descripcion" },
                values: new object[,]
                {
                    { new Guid("33333333-6666-6666-6666-333333333333"), 8, "Cada 8 horas" },
                    { new Guid("44444444-6666-6666-6666-444444444444"), 12, "Cada 12 horas" }
                });

            migrationBuilder.InsertData(
                table: "HistoriasClinicas",
                columns: new[] { "Id", "Alergias", "Antecedentes", "GrupoSanguineo", "ObservacionesGenerales", "PacienteId" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), "Ninguna conocida", "Hipertensión controlada", "A+", "Paciente ingresa por guardia clínica.", new Guid("11111111-aaaa-aaaa-aaaa-111111111111") },
                    { new Guid("aaaaaaaa-7777-7777-7777-aaaaaaaaaaaa"), "Polvo, Ácaros", "Fractura de fémur en 2010", "B-", "Fisioterapia ocasional por dolor articular.", new Guid("77777777-2777-aaaa-aaaa-777777777777") },
                    { new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), "Ninguna conocida", "Asma en la infancia", "O-", "Paciente derivado de consultorios externos.", new Guid("22222222-bbbb-bbbb-bbbb-222222222222") },
                    { new Guid("bbbbbbbb-8888-8888-8888-bbbbbbbbbbbb"), "Amoxicilina", "Migrañas crónicas", "O+", "Tratamiento neurológico en curso.", new Guid("88888888-2878-bbbb-bbbb-888888888888") },
                    { new Guid("cccccccc-3333-3333-3333-cccccccccccc"), "Ibuprofeno", "Cirugía de apéndice en 2015", "B+", "Paciente ingresa por guardia clínica.", new Guid("33333333-2373-cccc-cccc-333333333333") },
                    { new Guid("cccccccc-9999-9999-9999-cccccccccccc"), "Ninguna conocida", "Colesterol alto", "AB-", "Dieta estricta y control cardiológico.", new Guid("99999999-2979-cccc-cccc-999999999999") },
                    { new Guid("dddddddd-1010-1010-1010-dddddddddddd"), "Picadura de abejas", "Episodio de anafilaxia en 2022", "A+", "Porta autoinyector de epinefrina.", new Guid("10101010-3080-dddd-dddd-101010101010") },
                    { new Guid("dddddddd-4444-4444-4444-dddddddddddd"), "Ninguna conocida", "Diabetes Tipo 2", "AB+", "Paciente ingresa por guardia clínica.", new Guid("44444444-2474-dddd-dddd-444444444444") },
                    { new Guid("eeeeeeee-5555-5555-5555-eeeeeeeeeeee"), "Penicilina", "Sin antecedentes clínicos de relevancia", "O+", "Paciente ingresa por guardia clínica.", new Guid("55555555-2575-eeee-eeee-555555555555") },
                    { new Guid("ffffffff-6666-6666-6666-ffffffffffff"), "Ninguna conocida", "Úlcera gástrica", "A-", "Paciente ingresa por guardia clínica.", new Guid("66666666-2676-ffff-ffff-666666666666") }
                });

            migrationBuilder.InsertData(
                table: "Medicamentos",
                columns: new[] { "Id", "Contraindicaciones", "DrogaGenerica", "EfectosAdversos", "NombreComercial", "Presentacion", "RequiereControl", "ViaAdministracion" },
                values: new object[,]
                {
                    { new Guid("cccccccc-3333-3333-3333-cccccccccccc"), null, "Amoxicilina", null, "Amoxidal", "Comprimidos", false, 1 },
                    { new Guid("dddddddd-4444-4444-4444-dddddddddddd"), null, "Ibuprofeno", null, "Ibuprofeno", "Comprimidos recubiertos", false, 1 },
                    { new Guid("eeeeeeee-5555-5555-5555-eeeeeeeeeeee"), null, "Paracetamol", null, "Paracetamol", "Comprimidos", false, 1 }
                });

            migrationBuilder.InsertData(
                table: "Medicos",
                columns: new[] { "Id", "Especialidad", "Matricula", "Nombre" },
                values: new object[,]
                {
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Clínica Médica", "MN-123456", "Dr. Alejandro Salas" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Terapia Intensiva", "MN-654321", "Dr. Yonatan Rojas" }
                });

            migrationBuilder.InsertData(
                table: "UnidadesMedida",
                columns: new[] { "Id", "Abreviatura", "Nombre" },
                values: new object[,]
                {
                    { new Guid("11111111-5555-5555-5555-111111111111"), "mg", "Miligramos" },
                    { new Guid("22222222-5555-5555-5555-222222222222"), "ml", "Mililitros" }
                });

            migrationBuilder.InsertData(
                table: "Diagnosticos",
                columns: new[] { "Id", "CodigoCie10", "FechaHora", "HistoriaClinicaId", "MedicoId", "Observaciones" },
                values: new object[,]
                {
                    { new Guid("11111111-dddd-dddd-dddd-111111111111"), "J01.9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Paciente presenta cuadro de congestión y dolor facial agudo." },
                    { new Guid("22222222-dddd-dddd-dddd-222222222222"), "I10", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Presión arterial elevada en múltiples tomas. Requiere inicio de tratamiento." }
                });

            migrationBuilder.InsertData(
                table: "EvolucionesClinicas",
                columns: new[] { "Id", "FechaHora", "HistoriaClinicaId", "MedicoId", "Observacion" },
                values: new object[] { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2026, 6, 16, 18, 0, 0, 0, DateTimeKind.Utc), new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), new Guid("00000000-0000-0000-0000-000000000000"), "Paciente refiere alivio leve del dolor facial tras la primera dosis de Amoxicilina. Afebril. Continúa en observación." });

            migrationBuilder.InsertData(
                table: "Tratamientos",
                columns: new[] { "Id", "DiagnosticoId", "Dosis", "Estado", "FechaFin", "FechaInicio", "FrecuenciaAdministracionId", "MedicamentoId", "Observaciones", "UnidadMedidaId" },
                values: new object[,]
                {
                    { new Guid("55555555-7777-7777-7777-555555555555"), new Guid("11111111-dddd-dddd-dddd-111111111111"), 500m, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-6666-6666-6666-333333333333"), new Guid("cccccccc-3333-3333-3333-cccccccccccc"), null, new Guid("11111111-5555-5555-5555-111111111111") },
                    { new Guid("66666666-7777-7777-7777-666666666666"), new Guid("22222222-dddd-dddd-dddd-222222222222"), 600m, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("44444444-6666-6666-6666-444444444444"), new Guid("dddddddd-4444-4444-4444-dddddddddddd"), null, new Guid("11111111-5555-5555-5555-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "AuditoriasIA",
                columns: new[] { "Id", "AlertaDetectada", "FechaHora", "FueForzado", "JustificacionClinica", "MensajeIA", "NivelRiesgo", "TratamientoId" },
                values: new object[] { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), true, new DateTime(2026, 6, 16, 10, 30, 0, 0, DateTimeKind.Utc), true, "El paciente presenta dolor agudo inmanejable. Se administrará dosis baja y se monitoreará la presión arterial cada 8 horas.", "⚠️ Precaución: El uso de AINEs (Ibuprofeno) puede aumentar la presión arterial en pacientes hipertensos.", "Medio", new Guid("66666666-7777-7777-7777-666666666666") });

            migrationBuilder.InsertData(
                table: "TratamientosDosis",
                columns: new[] { "Id", "EnfermeraId", "Estado", "FechaDelSistema", "FechaProgramada", "FechaSuministro", "MotivoOmision", "Observaciones", "TratamientoId" },
                values: new object[,]
                {
                    { new Guid("77777777-1111-8888-8888-777777777777"), null, 1, null, new DateTime(2026, 7, 6, 20, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("55555555-7777-7777-7777-555555555555") },
                    { new Guid("77777777-2222-8888-8888-777777777777"), null, 1, null, new DateTime(2026, 7, 7, 4, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("55555555-7777-7777-7777-555555555555") },
                    { new Guid("88888888-1111-8888-8888-888888888888"), null, 1, null, new DateTime(2026, 7, 6, 21, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("66666666-7777-7777-7777-666666666666") },
                    { new Guid("88888888-2222-8888-8888-888888888888"), null, 1, null, new DateTime(2026, 7, 7, 9, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("66666666-7777-7777-7777-666666666666") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriasIA_TratamientoId",
                table: "AuditoriasIA",
                column: "TratamientoId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosticos_CodigoCie10",
                table: "Diagnosticos",
                column: "CodigoCie10");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosticos_HistoriaClinicaId",
                table: "Diagnosticos",
                column: "HistoriaClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosticos_MedicoId",
                table: "Diagnosticos",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Enfermeras_Legajo",
                table: "Enfermeras",
                column: "Legajo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvolucionesClinicas_HistoriaClinicaId",
                table: "EvolucionesClinicas",
                column: "HistoriaClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_Matricula",
                table: "Medicos",
                column: "Matricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tratamientos_DiagnosticoId",
                table: "Tratamientos",
                column: "DiagnosticoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamientos_FrecuenciaAdministracionId",
                table: "Tratamientos",
                column: "FrecuenciaAdministracionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamientos_MedicamentoId",
                table: "Tratamientos",
                column: "MedicamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamientos_UnidadMedidaId",
                table: "Tratamientos",
                column: "UnidadMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamientosDosis_EnfermeraId",
                table: "TratamientosDosis",
                column: "EnfermeraId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamientosDosis_TratamientoId",
                table: "TratamientosDosis",
                column: "TratamientoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditorias");

            migrationBuilder.DropTable(
                name: "AuditoriasIA");

            migrationBuilder.DropTable(
                name: "EvolucionesClinicas");

            migrationBuilder.DropTable(
                name: "TratamientosDosis");

            migrationBuilder.DropTable(
                name: "Enfermeras");

            migrationBuilder.DropTable(
                name: "Tratamientos");

            migrationBuilder.DropTable(
                name: "Diagnosticos");

            migrationBuilder.DropTable(
                name: "FrecuenciasAdministracion");

            migrationBuilder.DropTable(
                name: "Medicamentos");

            migrationBuilder.DropTable(
                name: "UnidadesMedida");

            migrationBuilder.DropTable(
                name: "CatalogosCie10");

            migrationBuilder.DropTable(
                name: "HistoriasClinicas");

            migrationBuilder.DropTable(
                name: "Medicos");
        }
    }
}
