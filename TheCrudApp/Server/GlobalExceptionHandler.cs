using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TheCrudApp.Exceptions;

namespace TheCrudApp.Server;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails();

        switch (exception)
        {
            case NotFoundException e:
                problemDetails.Title = e.Message;
                problemDetails.Status = StatusCodes.Status404NotFound;
                break;
            default:
                problemDetails.Title = "An Unexpected Error Occurred";
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Detail = exception.Message;
                break;
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}