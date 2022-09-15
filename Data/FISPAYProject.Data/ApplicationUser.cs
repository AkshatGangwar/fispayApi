using Microsoft.AspNetCore.Identity;

namespace FISPAYProject.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string UserType { get; set; }
    }
}
