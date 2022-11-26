﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PhotoSite.Core.ExtException;
using System.Net;

namespace PhotoSite.WebApi.Infrastructure.Middlewares
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public CustomExceptionFilterAttribute()
        {
            Order = int.MaxValue - 10;
        }

        public override void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "";

            if (context.Exception is UserException userException)
            {
                message = userException.UserMessage;
                statusCode = HttpStatusCode.BadRequest;
            }

            context.Result = new ObjectResult(message)
            {
                StatusCode = (int)statusCode
            };
            context.ExceptionHandled = true;
        }
    }
}