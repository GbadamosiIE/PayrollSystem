
using Microsoft.AspNetCore.Identity;
using PayRollSystem.Data.Context;
using PayRollSystem.Domain.Entities;

namespace PayRollSystem.Api.Extentions
{
    public static class IdentityServiceExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<Employee, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = false;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<PayRollSystemContext>()
                .AddDefaultTokenProviders();
        }
    }
}
