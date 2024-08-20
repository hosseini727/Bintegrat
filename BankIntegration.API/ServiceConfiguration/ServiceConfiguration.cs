using System.Reflection;
using Asp.Versioning;
using BankIntegration.API.Behaviors;
using BankIntegration.Infra.Persistance;
using BankIntegration.Infra.Repository.ElasticRepository.Repository;
using BankIntegration.Infra.Repository.ElasticRepository.RepositoryInterface;
using BankIntegration.Infra.Repository.SQLRepository.Repository;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Infra.ThirdApi;
using BankIntegration.Infra.ThirdApi.ConvertAccount;
using BankIntegration.Infra.ThirdApi.Sheba;
using BankIntegration.Service.Contracts;
using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using BankIntegration.Service.Services;
using BankIntegration.Service.Validation.BankValidation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nest;

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

        //FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        //MediatR
        services.AddMediatR(cnf =>
        {
            cnf.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            cnf.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        //IHttpClientFactory
        services.AddHttpClient();

        // InMempryCache
        services.AddMemoryCache();
        
        
        //elasticSearch and Nest
        services.AddSingleton<IElasticClient>(sp =>
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("inquiry-sheba");

            return new ElasticClient(settings);
        });
        services.AddScoped(typeof(IElasticGenericRepository<>),typeof(ElasticGenericRepository<>));


        // Services
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IPeopleService, PeopleService>();
        services.AddTransient<IInquiryBankService, InquiryBankService>();
        services.AddTransient<IInquiryBankHttp, InquiryBankHttp>();
        services.AddTransient<IConvertAccountNoBankHttp, ConvertAccountNoBankHttp>();
        services.AddTransient<IAPIkeyService, APIkeyService>();
        services.AddTransient<IValidator<GetInquiryShebaQuery>, GetShebaInquiryValidator>();
        services.AddTransient<IValidator<ConvertAccountNoQuery>, ConvertAccountNoValidator>();
        return services;
    }
}