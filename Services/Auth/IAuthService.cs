using App.Repositories.User;
using App.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle_ServicesRecord.Services.User;

namespace App.Services.Auth
{
   public interface IAuthService
    {
        Task<Users?> RegisterAsync(UserDTO request);
        Task<string?> LoginAsync(UserDTO request);
    }
}
