using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.ExceptionHandler
{
    public class CriticalExceptionsHandler(ILogger<CriticalException>logger ) : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) 
        {
            if(exception is CriticalException)
            {
                logger.LogCritical("");
            }
            return ValueTask.FromResult(false);
        }
    }
}
