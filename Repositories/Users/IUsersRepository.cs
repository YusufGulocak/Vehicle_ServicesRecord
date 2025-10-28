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
        Task<List<Users>> GetAllAsync();
        
        void Update(Users users);
        void Delete(Users users);
    }
}
