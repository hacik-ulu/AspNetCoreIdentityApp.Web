using AspNetCoreIdentityApp.Web.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityApp.Web.Models.Database
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        //Bu constructor, dışarıdan (genelde Startup.cs veya Program.cs'de) gönderilen veritabanı konfigürasyonunu (DbContextOptions) alır ve üst sınıfa (IdentityDbContext) iletir.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
