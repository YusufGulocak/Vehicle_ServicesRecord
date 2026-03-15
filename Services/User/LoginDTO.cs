using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.User
{
    public class LoginDTO
    {
        public string UserName { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
    }
}
