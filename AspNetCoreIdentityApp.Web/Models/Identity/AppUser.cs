using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string? City { get; set; }
    }
}
