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
        public async Task<List<Users>> GetAllAsync() 
        {
            return await Context.Users.AsNoTracking().ToListAsync();    
        }
        public void Update(Users users) 
        {
            Context.Users.Update(users);
        }
        public void Delete(Users users) 
        {
            Context.Users.Remove(users);
        }
    }
}
