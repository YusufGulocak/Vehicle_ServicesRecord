using App.Repositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.User
{
    public interface IUsersRepository:IGenericRepository<Users>
    {
        Task AddAsync(Users user);

        Task<Users?> GetByIdAsync(int id);
        Task<List<Users>> GetAllAsync();

        void Delete(Users user);

        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);

        Task<Users?> GetByUsernameOrEmailAsync(string value);

        IQueryable<Users> GetAll();
    }
}
