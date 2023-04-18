﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunkering.Core.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int? CompanyId { get; set; }
        public int ElpsId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }
        public bool IsActive { get; set; }
        public bool ProfileComplete { get; set; }
        public DateTime? LastJobDate { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
