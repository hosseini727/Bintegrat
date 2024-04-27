using AutoMapper;
using BankIntegration.Domain.Entities;
using BankIntegration.Service.Model;

namespace BankIntegration.Service.MappingProfile.ProductMapping;

public class ProductResponseModelProfile : Profile
{
    public ProductResponseModelProfile()
    {
        CreateMap<NewPasargad_Product, ProductResponseModel>();
    }
}