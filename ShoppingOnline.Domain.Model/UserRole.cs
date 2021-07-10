using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingOnline.Domain.Model
{
    public class UserRole:IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
