namespace MiddleWareDemo;

public class RequestLoggingMiddleWare
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleWare> _logger;
    // private readonly ISomeService _someService;

    // public RequestLoggingMiddleWare(RequestDelegate next, ILogger<RequestLoggingMiddleWare> logger, ISomeService someService)
    // {
    //     _next = next;
    //     _logger = logger;
    //     _logger.LogInformation("=====> Conventional middleware instantiated");
    //     _someService = someService;
    // }

    public RequestLoggingMiddleWare(RequestDelegate next, ILogger<RequestLoggingMiddleWare> logger)
    {
        _next = next;
        _logger = logger;
        _logger.LogInformation("=====> Conventional middleware instantiated");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"Request {context.Request.Method} {context.Request.Path}");
        // _someService.SomeMethod();

        await _next(context);

        _logger.LogInformation($"Response: {context.Response.StatusCode}");
    }

}