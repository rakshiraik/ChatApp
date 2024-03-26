using ChatBot.Common.Entry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Threading.Tasks;

namespace UserManagementService.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {

                await _next(httpContext);

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var e = new EntryResponse();
            switch (exception)
            {

                case DBConcurrencyException:
                    e.IsValid = false;
                    e.ConcurrencyViolation = true;
                    e.ErrorModel = new ErrorModel { Exceptions = ToDictionary(exception) };
                    break;
                case Exception:
                    e.IsValid = true;
                    e.ErrorModel = new ErrorModel { Exceptions = ToDictionary(exception) };
                    break;
                default:
                    e.IsValid = false;
                    e.ErrorModel = new ErrorModel { Exceptions = ToDictionary(exception) };
                    break;
            }
            string r = JsonConvert.SerializeObject(exception);
            await httpContext.Response.WriteAsync(r);
        }

        private Dictionary<string, string> ToDictionary(Exception ex)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            if (ex.Message != null)
            {
                d.Add("message", ex.Message);
            }

            if (ex.InnerException != null)
            {
                d.Add("InnerException", ex.InnerException.Message);
            }

            return d;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
