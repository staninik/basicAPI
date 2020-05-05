using BasicAPI.Exceptions;
using BasicAPI.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BasicAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
         
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.NotFound);
            }
            catch (NotUniqueException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
            }
        }

        #region Private methods
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, HttpStatusCode httpStatusCode)
        {
            int statusCode = (int)httpStatusCode;
            var error = new ErrorModel(exception.Message, statusCode);

            httpContext.Response.ContentType = MediaTypeNames.Application.Json;
            httpContext.Response.StatusCode = statusCode;

            var content = JsonConvert.SerializeObject(error);
            await httpContext.Response.WriteAsync(content);
        }
        #endregion
    }
}