using App.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*namespace App.Services.Filters
{
    public class NotFoundFilters<T,TId>(GenericRepository<T> genericRepository) : Attribute, IAsyncActionFilter where T : class where TId : struct
    {

    }
    {
       
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next();
                /*context.HttpContext.Response.StatusCode = 404;
                return;*/
            
           /* if(idValue is not  TId id  )
        {
            await next();
            return;
        }
           
            var hasEntity = await GenericRepository.AnyAsync(idValue as);
            await next();
        }
    }
}*/
