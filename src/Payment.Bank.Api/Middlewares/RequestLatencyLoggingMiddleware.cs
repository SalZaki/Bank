using System.Diagnostics;

namespace Payment.Bank.Api.Middlewares;

internal sealed class RequestLatencyLoggingMiddleware(
    RequestDelegate next,
    ILogger<RequestLatencyLoggingMiddleware> logger)
{
    private readonly ILogger _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        this._logger.Log(LogLevel.Information, "Started executing the request with {Id}: ", context.TraceIdentifier);

        var stopwatch = new Stopwatch();

        stopwatch.Start();

        await next(context);

        stopwatch.Stop();

        var time = stopwatch.ElapsedMilliseconds;

        this._logger.Log(LogLevel.Information, "Finished executing the request with {Id}: in {time} ms.", context.TraceIdentifier, time);
    }
}
