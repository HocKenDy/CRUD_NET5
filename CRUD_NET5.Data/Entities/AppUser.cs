using Microsoft.AspNetCore.Identity;
using System;

namespace CRUD_NET5.Data.Entities
{
    public class AppUser: IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
    }
}
