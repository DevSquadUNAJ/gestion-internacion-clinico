using Clinico.Aplicacion.Excepciones;
using Clinico.Application.DTOs.Respuestas;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clinico.API.Middlewares;

public class ManejadorGlobalExcepcionesMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ManejadorGlobalExcepcionesMiddleware> _logger;

    public ManejadorGlobalExcepcionesMiddleware(
        RequestDelegate next,
        ILogger<ManejadorGlobalExcepcionesMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ExceptionNotFound ex)
        {
            _logger.LogWarning("Entidad no encontrada: {Mensaje}", ex.Message);
            await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
        }
        catch (ExceptionBadRequest ex)
        {
            _logger.LogWarning("Bad Request: {Mensaje}", ex.Message);
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ha ocurrido un error crítico no controlado en la API.");
            await HandleExceptionAsync(context,ex, HttpStatusCode.InternalServerError);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var responseModel = new ErrorApiRespuesta
        {
            Mensaje = statusCode == HttpStatusCode.InternalServerError
                ? "Ocurrió un error interno en el servidor. Por favor, intente más tarde."
                : exception.Message
        };

        var result = JsonSerializer.Serialize(responseModel);
        return context.Response.WriteAsync(result);
    }
}