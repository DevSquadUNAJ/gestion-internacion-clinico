namespace Clinico.Infraestructura.IA.Configuracion
{
    public class OpcionesIA
    {
        public const string SeccionConfiguracion = "IA";

        /// <summary>"Gemini", "Groq" o "Mock"</summary>
        public string Proveedor { get; set; } = "Mock";

        public GeminiOpciones Gemini { get; set; } = new();
        public GroqOpciones Groq { get; set; } = new();

        public int TimeoutSegundos { get; set; } = 8;
    }

    public class GeminiOpciones
    {
        public string ApiKey { get; set; } = string.Empty;
        public string Modelo { get; set; } = "gemini-flash-latest";
        public string EndpointBase { get; set; } = "https://generativelanguage.googleapis.com/v1beta";
    }

    public class GroqOpciones
    {
        public string ApiKey { get; set; } = string.Empty;
        public string Modelo { get; set; } = "llama-3.3-70b-versatile";
        public string EndpointBase { get; set; } = "https://api.groq.com/openai/v1";
    }
}