
using System.Diagnostics;

namespace PersonalBlog.CategoryService.Api.Middlewares.RequestLogging;

public class ElasticRequestLoggingMiddleware
{
    private readonly Logging.ILogger _logger;
    private readonly RequestDelegate _next;

    public ElasticRequestLoggingMiddleware(Logging.ILogger logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        long start = Stopwatch.GetTimestamp();
        await _next(context);
        long end = Stopwatch.GetTimestamp();

        double elaspsedMilliSeconds = Stopwatch.GetElapsedTime(start, end).TotalMilliseconds;

        await _logger.LogInformation(
            new RequestInterceptionLog(
                context.Request.Method,
                context.Request.Path.Value!,
                context.Response.StatusCode,
                elaspsedMilliSeconds,
                context.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "...")
            );
    }


    public record RequestInterceptionLog(
        string requestMethod,
        string path,
        int statusCode,
        double elapsedMilliSeconds,
        string userIp);
}
