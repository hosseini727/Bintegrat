using AutoMapper;
using BankIntegration.Infra.ElasticMapping;
using BankIntegration.Service.Model.BankInquiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankIntegration.Service.MappingProfile.BankMapping
{   
    public class ConvertAccountIndexProfile : Profile
    {
        public ConvertAccountIndexProfile()
        {
            CreateMap<ConvertAccountInquiry, ConvertAccountNoResponseModel>();
            CreateMap<ConvertAccountNoResponseModel, ConvertAccountInquiry >();

        }
    }

}
