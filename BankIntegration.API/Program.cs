using BankIntegration.API.ServiceConfiguration;
using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Infra.SharedModel.Identity;
using BankIntegration.Service.Contracts;
using BankIntegration.Service.MiddleWare.Exception;
using BankIntegration.Service.Services;


var builder = WebApplication.CreateBuilder(args);

// add bankSetting
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();  
builder.Services.Configure<BankSettingModel>(builder.Configuration.GetSection("bankSettings"));
builder.Services.AddTransient<BankSettingModel>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddServices(builder.Configuration);


var siteSetting = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));


#region Add_Identity
builder.Services.AddCustomIdentity(siteSetting.IdentitySettings);
#endregion

builder.Services.AddJwtAuthentication(siteSetting.JwtSettings);
builder.Services.AddScoped<IJwtService, JwtService>();

// MassTransit
builder.Services.AddMassTransitWithRabbitMq();


// HttpClient
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

