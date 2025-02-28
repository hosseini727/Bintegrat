﻿using BankIntegration.Infra.Repository.SQLRepository.Persistance;
using BankIntegration.Infra.SharedModel.Identity;
using Microsoft.AspNetCore.Identity;
using SOS.Domain.Entities;

namespace BankIntegration.API.ServiceConfiguration
{
    public static class IdentityConfigurationExtension
    {
        public static void AddCustomIdentity(this IServiceCollection services, IdentitySettings settings)
        {
            services.AddIdentity<People, Role>(options =>
            {
                options.Password.RequireDigit = false; // میتواند کاراکتر عددی نداشته باشد 
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false; // @#
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

        }
    }

}
