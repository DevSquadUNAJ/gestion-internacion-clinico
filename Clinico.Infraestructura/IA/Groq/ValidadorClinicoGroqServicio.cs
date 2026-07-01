using Clinico.Aplicacion.DTOs.Respuestas.IA;
using Clinico.Aplicacion.DTOs.Solicitudes.IA;
using Clinico.Aplicacion.Interfaces.IExternos;
using Clinico.Infraestructura.IA.Configuracion;
using Clinico.Infraestructura.IA.Gemini;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.IA.Groq
{
    public class ValidadorClinicoGroqServicio : IValidadorClinicoIA
    {
        private readonly HttpClient _httpClient;
        private readonly OpcionesIA _opciones;
        private readonly ILogger<ValidadorClinicoGroqServicio> _logger;

        public ValidadorClinicoGroqServicio(
            HttpClient httpClient,
            IOptions<OpcionesIA> opciones,
            ILogger<ValidadorClinicoGroqServicio> logger)
        {
            _httpClient = httpClient;
            _opciones = opciones.Value;
            _logger = logger;
        }

        public async Task<AnalisisIAResultado> AnalizarAsync(
            ContextoClinicoIADto contexto,
            CancellationToken cancellationToken)
        {
            using var ctsTimeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            ctsTimeout.CancelAfter(TimeSpan.FromSeconds(_opciones.TimeoutSegundos));

            try
            {
                var url = $"{_opciones.Groq.EndpointBase}/chat/completions";

                _logger.LogInformation("📡 Invocando Groq en: {Url} (modelo {Modelo})",
                    url, _opciones.Groq.Modelo);

                var prompt = PromptBuilder.Construir(contexto);

                // Groq usa el formato OpenAI: messages con roles.
                var requestBody = new
                {
                    model = _opciones.Groq.Modelo,
                    messages = new[]
                    {
                        new
                        {
                            role = "system",
                            content = "Eres un asistente clínico que valida prescripciones médicas y " +
                                      "respondes SIEMPRE con un único objeto JSON válido, sin texto adicional."
                        },
                        new
                        {
                            role = "user",
                            content = prompt
                        }
                    },
                    temperature = 0.2,
                    response_format = new { type = "json_object" }  // fuerza salida JSON
                };

                var jsonOptionsRequest = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower
                };

                // La API key de Groq va en el header Authorization: Bearer
                using var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _opciones.Groq.ApiKey);
                request.Content = JsonContent.Create(requestBody, options: jsonOptionsRequest);

                var response = await _httpClient.SendAsync(request, ctsTimeout.Token);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync(ctsTimeout.Token);
                    _logger.LogError("❌ Groq status {StatusCode}: {Error}", response.StatusCode, error);
                    return new AnalisisIAResultado
                    {
                        Exitoso = false,
                        FueTimeout = false,
                        ErrorMensaje = $"Groq status {response.StatusCode}: {error}"
                    };
                }

                var groqResponse = await response.Content.ReadFromJsonAsync<GroqRespuesta>(
                    cancellationToken: ctsTimeout.Token);

                var textoCrudo = groqResponse?.Choices?[0]?.Message?.Content;

                if (string.IsNullOrWhiteSpace(textoCrudo))
                {
                    _logger.LogWarning("Groq devolvió una respuesta vacía.");
                    return new AnalisisIAResultado
                    {
                        Exitoso = false,
                        FueTimeout = false,
                        ErrorMensaje = "Respuesta vacía de Groq"
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
                        ErrorMensaje = "No se pudo deserializar la respuesta de Groq"
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
                _logger.LogWarning("Groq excedió el timeout de {Timeout}s.", _opciones.TimeoutSegundos);
                return new AnalisisIAResultado
                {
                    Exitoso = false,
                    FueTimeout = true,
                    ErrorMensaje = "Timeout invocando Groq"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error invocando Groq.");
                return new AnalisisIAResultado
                {
                    Exitoso = false,
                    FueTimeout = false,
                    ErrorMensaje = ex.Message
                };
            }
        }

        // DTOs internos para deserializar la respuesta de Groq (formato OpenAI)
        private class GroqRespuesta
        {
            public Choice[]? Choices { get; set; }

            public class Choice
            {
                public Message? Message { get; set; }
            }

            public class Message
            {
                public string? Content { get; set; }
            }
        }
    }
}