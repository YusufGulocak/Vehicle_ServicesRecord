using App.Services.Vehicles.Create;
using App.Services.Vehicles.Update;
using App.Services.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositories.User;
using App.Services.User.Create;
using Vehicle_ServicesRecord.Services.User;

namespace App.Services.User
{
    public interface IUserService
    {

        Task<ServiceResult<CreateUserResponse>> CreateAsync(CreateUserRequest request);

        Task<ServiceResult<List<UserDTO>>> GetAllAsync();
        Task<ServiceResult<UserDTO>> GetByIdAsync(int id);

        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);

        Task<Users?> GetByUsernameOrEmailAsync(string usernameOrEmail);

    }
}
