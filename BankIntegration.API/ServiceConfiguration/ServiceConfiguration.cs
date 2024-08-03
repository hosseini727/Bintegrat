using System.Reflection;
using Asp.Versioning;
using BankIntegration.API.Behaviors;
using BankIntegration.Infra.Persistance;
using BankIntegration.Infra.Repository.SQLRepository.Repository;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Infra.ThirdApi;
using BankIntegration.Service.Contracts;
using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using BankIntegration.Service.Services;
using BankIntegration.Service.Validation.BankValidation;
using FluentValidation;
using MediatR;
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
            options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

        //AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        //MediatR
        services.AddMediatR(cnf =>
        {
            cnf.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            cnf.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        //IHttpClientFactory
        services.AddHttpClient();

        // Services
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IPeopleService, PeopleService>();
        services.AddTransient<IInquiryBankService, InquiryBankService>();
        services.AddTransient<IBankHttp, BankHttp>();
        return services;
    }
}