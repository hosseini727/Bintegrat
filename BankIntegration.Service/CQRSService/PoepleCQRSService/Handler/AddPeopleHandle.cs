using AutoMapper;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Service.CQRSService.PoepleTransaction.Command;
using BankIntegration.Service.Model.People.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SOS.Domain.Entities;

namespace BankIntegration.Service.CQRSService.PoepleTransaction.Handler;

public class AddPeopleHandle : IRequestHandler<AddPeopleCommand, AddPeopleResponseModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<People> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public AddPeopleHandle(IUnitOfWork unitOfWork, IMapper mapper, UserManager<People> userManager, RoleManager<Role> roleManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<AddPeopleResponseModel> Handle(AddPeopleCommand request, CancellationToken cancellationToken)
    {
        var people = _mapper.Map<People>(request.AddPeopleModel);

        var createUserResult = await _userManager.CreateAsync(people);

        if (createUserResult.Succeeded)
        {
            var addPasswordResult = await _userManager.AddPasswordAsync(people, request.AddPeopleModel.Password);

            if (addPasswordResult.Succeeded)
            {
                return new AddPeopleResponseModel() { IsSuccess = true, Message = "User created successfully." };
            }
            else
            {
                throw new Exception($"Error adding password for user: {people.UserName}. Details: {string.Join(", ", addPasswordResult.Errors)}");
            }
        }
        else
        {
            throw new Exception($"Error creating user: {people.UserName}. Details: {string.Join(", ", createUserResult.Errors)}");
        }
    }
}