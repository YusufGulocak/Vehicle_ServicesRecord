using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vehicle_ServicesRecord.Services.User
{
    public class UserDTO
    {
        

        public string UserName { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Email { get; set; } = default!;
        
       
        // LastLogin isteğe bağlı, eğer ekranda göstermeyeceksen çıkarabilirsin.
        
    }
}
