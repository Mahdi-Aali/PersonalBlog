using PersonalBlog.CategoryService.Api.Helpers;

namespace PersonalBlog.CategoryService.Api.Middlewares.ErrorHandling;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly Logging.ILogger _logger;

    public ExceptionHandler(RequestDelegate requestDelegate, Logging.ILogger logger)
    {
        _next = requestDelegate;
        _logger = logger;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var logResult = await _logger.LogError(ex, "Something when wring when processing request");

            ApiResponseBuilder<object> builder = new();

            ApiResponse<object> response = builder
                .SetHeaders(
                new(StatusCodes.Status500InternalServerError, 
                ["Something went wrong when processing your request. please give the error tracking code to support for further informations and helps.", 
                $"Error tracking code: {logResult.EXID}"]))
                .SetPayload(null!)
                .Build();

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
