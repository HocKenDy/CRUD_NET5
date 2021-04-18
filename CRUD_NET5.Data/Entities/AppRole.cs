using Microsoft.AspNetCore.Identity;
using System;

namespace CRUD_NET5.Data.Entities
{
    public class AppRole: IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
