using BankIntegration.API.ServiceConfiguration;
using BankIntegration.Infra.Repository.SQLRepository.Interface;
using BankIntegration.Infra.SharedModel;
using BankIntegration.Service.MiddleWare.Exception;
using BankIntegration.Service.Utility.Jwt;

var builder = WebApplication.CreateBuilder(args);

// add bankSettig
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();  
builder.Services.Configure<BankSettingModel>(builder.Configuration.GetSection("bankSettings"));
builder.Services.AddTransient<BankSettingModel>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddServices(builder.Configuration);


var _siteSetting = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));


#region Add_Identity
builder.Services.AddCustomIdentity(_siteSetting.IdentitySettings);
#endregion

builder.Services.AddJwtAuthentication(_siteSetting.JwtSettings);
builder.Services.AddScoped<IJwtService, JwtService>();


// HttpClient
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

