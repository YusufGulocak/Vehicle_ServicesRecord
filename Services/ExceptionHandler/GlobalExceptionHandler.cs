using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace App.Services.ExceptionHandler
{
    public class GlobalExceptionHandler:IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
            {
                var errorAsDto=ServiceResult.Fail(exception.Message, HttpStatusCode.InternalServerError);
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken: cancellationToken);
            return true;
        }
        
    }
}
    