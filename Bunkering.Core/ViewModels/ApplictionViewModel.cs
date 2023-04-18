using System.ComponentModel.DataAnnotations;

namespace Bunkering.Core.ViewModels
{
    public class ApplictionViewModel
    {
        [Display(Name = "Facility Type")]
        public int FacilityTypeId { get; set; }
        [Display(Name = "Application Type")]
        public int ApplicationTypeId { get; set; }
        [Display(Name = "Bunker (Facility) Name")]
        public string FacilityName { get; set; }
        public string Address { get; set; }
        public string? LicenseNo { get; set; }
        [Display(Name = "State")]
        public int StateId { get; set; }
        [Display(Name = "LGA")]
        public int LgaId { get; set; }
    }
}
