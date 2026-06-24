using Clinico.Aplicacion.DTOs.Solicitudes.IA;
using System.Text.Json;

namespace Clinico.Infraestructura.IA.Gemini
{
    internal static class PromptBuilder
    {
        public static string Construir(ContextoClinicoIADto contexto)
        {
            var contextoJson = JsonSerializer.Serialize(contexto, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return $@"Eres un asistente clínico que valida prescripciones médicas.

Analiza el siguiente contexto clínico de un paciente internado y la prescripción propuesta por el médico tratante.
Detecta riesgos relacionados con alergias, interacciones medicamentosas, dosis inadecuadas o contraindicaciones.

Devuelve SIEMPRE un único objeto JSON que cumpla EXACTAMENTE esta estructura (sin texto fuera del JSON):

{{
  ""nivelRiesgo"": ""Bajo|Medio|Alto|Critico"",
  ""alertaDetectada"": true,
  ""alertas"": [
    {{
      ""tipo"": ""Alergia|Interaccion|Dosis|Contraindicacion"",
      ""severidad"": ""Baja|Media|Alta|Critica"",
      ""descripcion"": ""texto""
    }}
  ],
  ""sugerencia"": {{
    ""aplicar"": false,
    ""medicamentoAlternativo"": ""texto o null"",
    ""dosis"": 0,
    ""unidad"": ""texto o null"",
    ""frecuencia"": ""texto o null"",
    ""justificacion"": ""texto""
  }},
  ""resumenClinico"": ""1-2 oraciones para el médico tratante en español""
}}

Reglas:
- nivelRiesgo: 'Bajo' si no hay alertas; 'Medio' alertas leves; 'Alto' relevantes; 'Critico' riesgo grave.
- alertaDetectada: true si el array alertas tiene elementos.
- Los valores de nivelRiesgo, tipo y severidad DEBEN ser exactamente uno de los indicados (respetando mayúsculas).
- sugerencia puede ser null si no hay cambio que sugerir.

CONTEXTO:
{contextoJson}";
        }
    }
}