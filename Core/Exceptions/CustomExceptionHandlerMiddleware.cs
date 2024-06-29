using System.Net;
using Core.Response.Error;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Exceptions
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var code = HttpStatusCode.InternalServerError;



            switch (ex)
            {
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
                case UnAuthorizationException _:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return Task.CompletedTask;
                case ForbiddenException _:
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return Task.CompletedTask;
                case DublicatedEntityException _:
                    code = HttpStatusCode.Conflict;
                    break;
                case BadRequestException _:
                    code = HttpStatusCode.BadRequest;
                    break;
            }

            
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(new ErrorResponse { Message = ex.Message }.ToString());
        }
    }
}
