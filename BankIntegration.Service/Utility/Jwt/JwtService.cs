using BankIntegration.Infra.Repository.SQLRepository.Interface;
using BankIntegration.Infra.SharedModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankIntegration.Service.Utility.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly SiteSettings _siteSetting;
        private readonly UserManager<People> _userManager;

        public JwtService(IOptions<SiteSettings> settings, UserManager<People> userManager)
        {
            _siteSetting = settings.Value;
            _userManager = userManager;
        }

        public string GenerateJwtToken(People people)
        {

            var userRoles = _userManager.GetRolesAsync(people).Result;
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_siteSetting.JwtSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]{
                new Claim("Id", people.Id.ToString()),
                new Claim(ClaimTypes.Name ,people.UserName ),
                new Claim(JwtRegisteredClaimNames.Email, people.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(userRoles.Select(role => new Claim(ClaimTypes.Role, role)))),
                Issuer = _siteSetting.JwtSettings?.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(5),
                Audience = _siteSetting.JwtSettings?.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;

        }
    }

}
