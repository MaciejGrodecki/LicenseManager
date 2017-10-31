using System;
using System.Net;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LicenseManager.Infrastructure.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var exceptionType = exception.GetType();
            var statusCode = HttpStatusCode.InternalServerError;
            var errorCode = "error";

            switch(exception)
            {
                case  LicenseManagerException e when exceptionType == typeof(LicenseManagerException):
                    errorCode = e.ErrorCode;
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
            }

            var response = new { message = exception.Message, errorCode = errorCode };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}