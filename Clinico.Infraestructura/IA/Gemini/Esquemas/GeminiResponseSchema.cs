namespace Clinico.Infraestructura.IA.Gemini.Esquemas
{
    internal static class GeminiResponseSchema
    {
        public static object Construir()
        {
            return new
            {
                type = "object",
                properties = new
                {
                    nivelRiesgo = new
                    {
                        type = "string",
                        @enum = new[] { "Bajo", "Medio", "Alto", "Critico" }
                    },
                    alertaDetectada = new { type = "boolean" },
                    alertas = new
                    {
                        type = "array",
                        items = new
                        {
                            type = "object",
                            properties = new
                            {
                                tipo = new { type = "string", @enum = new[] { "Alergia", "Interaccion", "Dosis", "Contraindicacion" } },
                                severidad = new { type = "string", @enum = new[] { "Baja", "Media", "Alta", "Critica" } },
                                descripcion = new { type = "string" }
                            },
                            required = new[] { "tipo", "severidad", "descripcion" }
                        }
                    },
                    sugerencia = new
                    {
                        type = "object", // Eliminado: nullable = true
                        properties = new
                        {
                            aplicar = new { type = "boolean" },
                            medicamentoAlternativo = new { type = "string" }, // Eliminado: nullable = true
                            dosis = new { type = "number" },                // Eliminado: nullable = true
                            unidad = new { type = "string" },               // Eliminado: nullable = true
                            frecuencia = new { type = "string" },           // Eliminado: nullable = true
                            justificacion = new { type = "string" }
                        },
                        required = new[] { "aplicar", "justificacion" } // Al no estar los otros, ya se consideran opcionales
                    },
                    resumenClinico = new { type = "string" }
                },
                required = new[] { "nivelRiesgo", "alertaDetectada", "alertas", "resumenClinico" }
            };
        }
    }
}