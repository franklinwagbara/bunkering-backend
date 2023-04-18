using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bunkering.Core.Data
{
    public class Facility
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ElpsId { get; set; }
        public int FacilityTypeId { get; set; }
        public string Name { get; set; }
        public int LgaId { get; set; }
        public string Address { get; set; }
        public bool IsLicensed { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; }
        [ForeignKey(nameof(FacilityTypeId))]
        public FacilityType FacilityType { get; set; }
        [ForeignKey(nameof(LgaId))]
        public LGA LGA { get; set; }
    }
}
