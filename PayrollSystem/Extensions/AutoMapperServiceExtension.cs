
using Microsoft.Extensions.DependencyInjection;
using PayRollSystem.Api.Profiles;


namespace PayRollSystem.Api.Extentions
{
    public static class AutoMapperServiceExtension
    {
        public static void ConfigureAutoMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapInitializer));
        }
    }
}
