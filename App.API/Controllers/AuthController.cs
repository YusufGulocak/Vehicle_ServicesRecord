using App.Repositories;
using App.Repositories.User;
using App.Services.Auth;
using App.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Vehicle_ServicesRecord.Services.User;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        
        [HttpPost("register")]
       
        public async Task<IActionResult> Register(UserDTO request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
            {
                return BadRequest("Username already exists.");
            }
            return Ok(user);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO request)
        {
            var token = await authService.LoginAsync(request);
            if (token is null)
            {
                return BadRequest("Invalid username or password.");
            }
            return Ok(new { Token = token });
        }
        [Authorize]
        [HttpGet]
        public IActionResult AuthenticationOnlyEndpoint()
        {
            return Ok("You have accessed an authentication-only endpoint.");
        }
    }
}
