using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Auth
{
    using App.Repositories;
    using App.Repositories.User;
    using App.Services.User;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Routing.Tree;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.Data;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Vehicle_ServicesRecord.Services.User;

    public class AuthService(AppDbContext context,IConfiguration configuration) : IAuthService
    {
        public async Task<string?> LoginAsync(UserDTO request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user is null)
            {
                return null;
            }


            if (new PasswordHasher<Users>().VerifyHashedPassword(user, user.PasswordHash, request.PasswordHash)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }
            var token =CreateToken(user);
            return token;


        }
       
        
        
         public async Task<Users?> RegisterAsync(UserDTO request)
         {
            try
            {
                if (await context.Users.AnyAsync(u => u.UserName == request.UserName))
                {
                    return null;
                }
                var user = new Users();
                var hashedPassword = new PasswordHasher<Users>()
                    .HashPassword(user, request.PasswordHash);
                user.UserName = request.UserName;
                user.Email = request.Email;
                user.Role = "User";
                user.PasswordHash = hashedPassword;

                context.Users.Add(user);
                await context.SaveChangesAsync();
                return user;


            }
            
            
                catch (DbUpdateException ex)
            {
                // Inner exception'ı konsola yazdır
                Console.WriteLine($"DbUpdateException: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");

                // Daha detaylı bilgi
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Stack Trace: {ex.InnerException.StackTrace}");
                }

                throw; // Hatayı yukarı fırlat ki controller'da görelim
            }
        }
        private string CreateToken(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())

            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
              .GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        }
    }

}
