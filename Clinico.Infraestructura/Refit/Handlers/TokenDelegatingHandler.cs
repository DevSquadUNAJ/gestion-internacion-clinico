using Clinico.Aplicacion.Interfaces.ISeguridad;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Refit.Handlers
{
    public class TokenDelegatingHandler : DelegatingHandler
    {
        private readonly ITokenUsuarioActual _tokenUsuarioActual;

        public TokenDelegatingHandler(ITokenUsuarioActual tokenUsuarioActual)
        {
            _tokenUsuarioActual = tokenUsuarioActual;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _tokenUsuarioActual.ObtenerToken();

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}