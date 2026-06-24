using Clinico.Aplicacion.DTOs.Respuestas.IA;
using Clinico.Aplicacion.DTOs.Solicitudes.IA;
using Clinico.Aplicacion.Interfaces.IExternos;
using Clinico.Infraestructura.IA.Configuracion;
using Clinico.Infraestructura.IA.Gemini.Esquemas;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.IA.Gemini
{
    public class ValidadorClinicoGeminiServicio : IValidadorClinicoIA
    {
        private readonly HttpClient _httpClient;
        private readonly OpcionesIA _opciones;
        private readonly ILogger<ValidadorClinicoGeminiServicio> _logger;

        public ValidadorClinicoGeminiServicio(
            HttpClient httpClient,
            IOptions<OpcionesIA> opciones,
            ILogger<ValidadorClinicoGeminiServicio> logger)
        {
            _httpClient = httpClient;
            _opciones = opciones.Value;
            _logger = logger;

            // Agrega esta línea para evitar el rechazo de los servidores de Google:
            if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "ClinicoBackendApp/1.0");
            }
        }

        public async Task<AnalisisIAResultado> AnalizarAsync(
            ContextoClinicoIADto contexto,
            CancellationToken cancellationToken)
        {

            using var ctsTimeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            ctsTimeout.CancelAfter(TimeSpan.FromSeconds(_opciones.TimeoutSegundos));

            try
            {
                var url = $"{_opciones.Gemini.EndpointBase}/models/{_opciones.Gemini.Modelo}:generateContent?key={_opciones.Gemini.ApiKey}";

                _logger.LogInformation("📡 Invocando Gemini en: {Url}", url.Replace(_opciones.Gemini.ApiKey, "***"));

                var prompt = PromptBuilder.Construir(contexto);
                var schema = GeminiResponseSchema.Construir();

                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[] { new { text = prompt } }
                        }
                    },
                    generationConfig = new
                    {
                        responseMimeType = "application/json",
                        responseSchema = schema,
                        temperature = 0.2
                    }
                };

                // IMPORTANTE: Gemini espera todos los campos en camelCase.
                var jsonOptionsRequest = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
                };

                // LOG TEMPORAL DE DIAGNÓSTICO
                var bodyParaLog = JsonSerializer.Serialize(requestBody, jsonOptionsRequest);
                _logger.LogInformation("🔍 Body enviado a Gemini: {Body}", bodyParaLog);
                _logger.LogInformation("🔍 Headers del HttpClient: {Headers}",
                    string.Join(", ", _httpClient.DefaultRequestHeaders.Select(h => h.Key)));

                var response = await _httpClient.PostAsJsonAsync(url, requestBody, jsonOptionsRequest, ctsTimeout.Token);

                /*if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync(ctsTimeout.Token);
                    _logger.LogWarning("Gemini respondió con código {StatusCode}: {Error}",
                        response.StatusCode, error);

                    return new AnalisisIAResultado
                    {
                        Exitoso = false,
                        FueTimeout = false,
                        ErrorMensaje = $"Gemini status {response.StatusCode}"
                    };
                }*/
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync(ctsTimeout.Token);
                    _logger.LogError(
                        "❌ Gemini respondió con código {StatusCode}. URL llamada: {Url}. Body de error: {Error}",
                        response.StatusCode, url.Replace(_opciones.Gemini.ApiKey, "***"), error);

                    return new AnalisisIAResultado
                    {
                        Exitoso = false,
                        FueTimeout = false,
                        ErrorMensaje = $"Gemini status {response.StatusCode}: {error}"
                    };
                }

                var geminiResponse = await response.Content.ReadFromJsonAsync<GeminiRespuesta>(
                    cancellationToken: ctsTimeout.Token);

                var textoCrudo = geminiResponse?.Candidates?[0]?.Content?.Parts?[0]?.Text;

                if (string.IsNullOrWhiteSpace(textoCrudo))
                {
                    _logger.LogWarning("Gemini devolvió una respuesta vacía.");
                    return new AnalisisIAResultado
                    {
                        Exitoso = false,
                        FueTimeout = false,
                        ErrorMensaje = "Respuesta vacía de Gemini"
                    };
                }

                
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
                };

                var analisis = JsonSerializer.Deserialize<AnalisisIADto>(textoCrudo, jsonOptions);

                if (analisis is null)
                {
                    return new AnalisisIAResultado
                    {
                        Exitoso = false,
                        FueTimeout = false,
                        ErrorMensaje = "No se pudo deserializar la respuesta de Gemini"
                    };
                }

                return new AnalisisIAResultado
                {
                    Analisis = analisis,
                    PayloadJsonCrudo = textoCrudo,
                    Exitoso = true,
                    FueTimeout = false
                };
            }
            catch (OperationCanceledException) when (ctsTimeout.IsCancellationRequested
                                                     && !cancellationToken.IsCancellationRequested)
            {
                _logger.LogWarning("Gemini excedió el timeout de {Timeout}s.", _opciones.TimeoutSegundos);
                return new AnalisisIAResultado
                {
                    Exitoso = false,
                    FueTimeout = true,
                    ErrorMensaje = "Timeout invocando Gemini"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error invocando Gemini.");
                return new AnalisisIAResultado
                {
                    Exitoso = false,
                    FueTimeout = false,
                    ErrorMensaje = ex.Message
                };
            }
        }

        // Clase interna para mapear la respuesta de Gemini (se puede separar)
        private class GeminiRespuesta
        {
            public Candidate[]? Candidates { get; set; }

            public class Candidate
            {
                public Content? Content { get; set; }
            }

            public class Content
            {
                public Part[]? Parts { get; set; }
            }

            public class Part
            {
                public string? Text { get; set; }
            }
        }
    }
}