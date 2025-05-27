
namespace MiddleWareDemo;

public class FactoryBasedMiddleware : IMiddleware
{
    private readonly ILogger<FactoryBasedMiddleware> _logger;
    private readonly ISomeService _someService;

    public FactoryBasedMiddleware(ILogger<FactoryBasedMiddleware> logger, ISomeService someService)
    {
        _logger = logger;
        _someService = someService;
        _logger.LogInformation("=====> Factory based middleware instantiated");
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _logger.LogInformation("Before request");
        _someService.SomeMethod();
        await next(context);

        _logger.LogInformation("After request");
    }
}