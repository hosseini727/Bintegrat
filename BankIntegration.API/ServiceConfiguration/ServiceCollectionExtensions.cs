using BankIntegration.Infra.SharedModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BankIntegration.API.ServiceConfiguration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretkey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
                var encryptionkey = Encoding.UTF8.GetBytes(jwtSettings.Encryptkey);

                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero, // default: 5 min
                    RequireSignedTokens = true, //توکن ارسالی امضا داشته باشد 

                    ValidateIssuerSigningKey = true, // حتما امضای مورد اعتبار سنجی و وولیدیت قرار بگیرد   
                    IssuerSigningKey = new SymmetricSecurityKey(secretkey),//توکنی که بدست ما میرسد باید منقضی نشده باشد

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true, //default : false // //صادر کننده 
                    ValidAudience = jwtSettings.Audience,//مصرف کننده 

                    ValidateIssuer = true, //default : false
                    ValidIssuer = jwtSettings.Issuer,
                    TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)

                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;

            });
        }
    }

}
