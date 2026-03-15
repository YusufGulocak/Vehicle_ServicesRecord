using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.User
{
    public class UsersRepository(AppDbContext context) : GenericRepository<Users>(context), IUsersRepository
    {
        public async Task AddAsync(Users user)
        {
            await context.Users.AddAsync(user);
        }

        public async Task<Users?> GetByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<List<Users>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public IQueryable<Users> GetAll()
        {
            return context.Users.AsQueryable();
        }

        public void Delete(Users user)
        {
            context.Users.Remove(user);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName == username);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<Users?> GetByUsernameOrEmailAsync(string value)
        {
            return await context.Users
                .FirstOrDefaultAsync(x =>
                    x.UserName == value || x.Email == value);
        }
    }
}
