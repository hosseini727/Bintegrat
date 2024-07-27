using SOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankIntegration.Infra.Repository.SQLRepository.Interface
{
    public interface IJwtService
    {
        string GenerateJwtToken(People people);
    }
}
