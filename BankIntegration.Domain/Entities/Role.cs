using BankIntegration.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.Domain.Entities
{
    public class Role : IdentityRole<long>, IEntity
    {
        [Required]
        [StringLength(100)]
        public required string Description { get; set; }
    }
}
    