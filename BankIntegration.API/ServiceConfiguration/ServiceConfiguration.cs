using System.Reflection;
using Asp.Versioning;

namespace BankIntegration.API.ServiceConfiguration;

public static class ServiceConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services , IConfiguration configuration)
    {
        //Api Versioning
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });
        
        //AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        //MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}