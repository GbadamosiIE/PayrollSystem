

using PayRollSystem.Domain.IRepositories;
using PayRollSystem.Data.Repositories;
using PayRollSystem.Domain.Utilities;
using PayRollSystem.Domain.IPayRollSystemServices;
using PayRollSystem.Domain.EmailService;
using HotelManagement.Core.Utilities;
using PayRollSystem.Domain.IServices;
using PayRollSystem.Data.Services;
using PayRollSystem.Service.Services;

namespace PayRollSystem.Api.Extensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Add Service Injections Here
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationServices>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenDetails, TokenDetails>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();



           
        

           

        }
    }
}

