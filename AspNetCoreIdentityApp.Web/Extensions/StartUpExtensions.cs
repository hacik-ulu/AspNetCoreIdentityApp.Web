using AspNetCoreIdentityApp.Web.CustomValidations;
using AspNetCoreIdentityApp.Web.Models.Database;
using AspNetCoreIdentityApp.Web.Models.Identity;

namespace AspNetCoreIdentityApp.Web.Extensions
{
    public static class StartUpExtensions
    {
        public static void AddIdentityWithExt(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>
            (options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefgjijklmnoprstuvwxyz123456789_";

                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

            }).AddPasswordValidator<PasswordValidator>().AddEntityFrameworkStores<AppDbContext>();

        }
    }
}
