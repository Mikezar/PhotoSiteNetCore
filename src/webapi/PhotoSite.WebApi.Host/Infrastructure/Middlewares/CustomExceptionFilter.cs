using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using PhotoSite.Core.ExtException;
using System.Net;

namespace PhotoSite.WebApi.Infrastructure.Middlewares
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;

        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = string.Empty;

            if (context.Exception is UserException userException)
            {
                message = userException.UserMessage;
                statusCode = HttpStatusCode.BadRequest;
            }

            var problemDetails = new ValidationProblemDetails()
            {
                Status = (int)statusCode,
                Title = statusCode.ToString(),
                Detail = message
            };

            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = (int)statusCode,
            };

            _logger.LogError(context.Exception, context.Exception.Message);
            context.ExceptionHandled = true;
        }
    }
}
