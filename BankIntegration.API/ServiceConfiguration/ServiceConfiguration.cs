using System.Reflection;
using Asp.Versioning;
using BankIntegration.Infra.Persistance;
using BankIntegration.Infra.Repository.SQLRepository.Repository;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Service.Contracts;
using BankIntegration.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace BankIntegration.API.ServiceConfiguration;

public static class ServiceConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
      

        //Api Versioning
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        // ApplicationDbContext
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(connectionString),ServiceLifetime.Scoped);
        
        //AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        //MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Services
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IProductService, ProductService>();
        return services;
    }
}