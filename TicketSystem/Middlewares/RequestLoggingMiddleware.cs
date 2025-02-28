﻿namespace TicketSystem.API.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var start = DateTime.UtcNow;
        await _next(context);
        var elapsed = DateTime.UtcNow - start;

        _logger.LogInformation("HTTP {Method} {Path} responded in {ElapsedMilliseconds}ms",
            context.Request.Method,
            context.Request.Path,
            elapsed.TotalMilliseconds);
    }
}
