using Microsoft.EntityFrameworkCore;

namespace App.Repositories.User
{
    public class UsersRepository(AppDbContext context) : GenericRepository<Users>(context), IUsersRepository
    {
        public async Task<List<Users>> GetAllAsync()
        {
            return await Context.Users.ToListAsync();
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await Context.Users.AnyAsync(x => x.UserName == username);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await Context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<Users?> GetByUsernameOrEmailAsync(string value)
        {
            return await Context.Users
                .FirstOrDefaultAsync(x => x.UserName == value || x.Email == value);
        }
    }
}
