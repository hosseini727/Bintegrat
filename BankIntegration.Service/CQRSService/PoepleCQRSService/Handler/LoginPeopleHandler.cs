using AutoMapper;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Infra.SharedModel.Identity;
using BankIntegration.Service.Contracts;
using BankIntegration.Service.CQRSService.PoepleTransaction.Command;
using BankIntegration.Service.Model.People.Response;
using BankIntegration.Service.Utility.Jwt;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SOS.Domain.Entities;


namespace BankIntegration.Service.CQRSService.PoepleTransaction.Handler
{
    public class LoginPeopleHandler : IRequestHandler<LoginPeopleCommand, loginPeopleResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtServices;
        private readonly SiteSettings _siteSetting;
        private readonly UserManager<People> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public LoginPeopleHandler(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtServices,
            IOptions<SiteSettings> settings, UserManager<People> userManager, RoleManager<Role> roleManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtServices = jwtServices;
            _siteSetting = settings.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        async Task<loginPeopleResponseModel> IRequestHandler<LoginPeopleCommand, loginPeopleResponseModel>.Handle(LoginPeopleCommand request, CancellationToken cancellationToken)
        {
            var findUser = await _userManager.FindByNameAsync(request.LoginPeopleModel.UserName);
            if (findUser == null)
            {
                return new loginPeopleResponseModel()
                {
                    IsSuccess = false,
                    Message = "There is no UserName with this profile",
                };
            }
            var isLogin = await _userManager.CheckPasswordAsync(findUser, request.LoginPeopleModel.Password);
            if (isLogin)
            {
                var newToken = _jwtServices.GenerateJwtToken(findUser);
                var refreshToken = GenerateToken.GenerateRefreshToken();
                _ = int.TryParse(_siteSetting.JwtSettings.RefreshTokenValidityInDays.ToString(), out int refreshTokenValidityInDays);
                findUser.RefreshToken = refreshToken;
                findUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                await _userManager.UpdateAsync(findUser);

                return new loginPeopleResponseModel()
                {
                    IsSuccess = true,
                    Message = "Success",
                    AccessToken = newToken,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = findUser.RefreshTokenExpiryTime,
                };
            }
            else
            {
                return new loginPeopleResponseModel()
                {
                    IsSuccess = false,
                    Message = "There is no user with this profile",
                };
            }
        }

    }

}
