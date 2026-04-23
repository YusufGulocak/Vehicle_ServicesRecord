using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.User
{
    public class Users : BaseEntity<int>
    {
        public string UserName { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Role { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
