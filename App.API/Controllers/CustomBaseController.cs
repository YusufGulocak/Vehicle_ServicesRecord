using App.Services;
using App.Services.Vehicles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {   
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> result,string? urlAsCreated=null)
        {
            return result.StatusCode switch
            {
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.Created when urlAsCreated is not null =>
                    Created(urlAsCreated, result),
                _ => new ObjectResult(result)
                {
                    StatusCode = (int)result.StatusCode
                }
            };


        }

        [NonAction]
        public IActionResult CreateActionResult(ServiceResult result)
        {
            return result.StatusCode switch
            {
                HttpStatusCode.NoContent => NoContent(),
                _ => new ObjectResult(result)
                {
                    StatusCode = (int)result.StatusCode
                }
            };

        }

     
    } }
