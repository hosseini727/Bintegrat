using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SOS.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Service.Model.People.Response;
using BankIntegration.Infra.Repository.SQLRepository.Interface;
using BankIntegration.Infra.SharedModel.Identity;
using BankIntegration.Service.Utility.Jwt;
using BankIntegration.Service.CQRSService.PoepleTransaction.Command;

namespace BankIntegration.Service.CQRSService.PoepleTransaction.Handler
{
    public class RereshTokenPeopleHandler : IRequestHandler<RefreshTokenPeopleCommand, RefreshTokenPeopleResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtServices;
        private readonly SiteSettings _siteSetting;
        private readonly UserManager<People> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RereshTokenPeopleHandler(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtServices,
           IOptions<SiteSettings> settings, UserManager<People> userManager, RoleManager<Role> roleManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtServices = jwtServices;
            _siteSetting = settings.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<RefreshTokenPeopleResponseModel> Handle(RefreshTokenPeopleCommand request, CancellationToken cancellationToken)
        {
            var principal = GetPrincipalFromExpiredToken(request.RefreshTokenPeopleModel.AccessToken);
            if (principal == null)
                return new RefreshTokenPeopleResponseModel() { IsSuccess = false, Message = "ناموفق " };
            var userNameClaim = principal.FindFirstValue(ClaimTypes.Name);
            var UserName = principal.Identities.First().Name;
            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null || user.RefreshToken != request.RefreshTokenPeopleModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return new RefreshTokenPeopleResponseModel() { IsSuccess = false, Message = "ناموفق " };
            var newToken = _jwtServices.GenerateJwtToken(user);
            var refreshToken = GenerateToken.GenerateRefreshToken();
            _ = int.TryParse(_siteSetting.JwtSettings.RefreshTokenValidityInDays.ToString(), out int refreshTokenValidityInDays);
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);
            return new RefreshTokenPeopleResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                AccessToken = newToken,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            };
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.SecretKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }

}
