using Bogus.DataSets;
using Garage3.Core;
using System.ComponentModel.DataAnnotations;

namespace Garage3.ViewModels
{
    public class VehicleIndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Registreringsnummer")]
        public string RegNo { get; set; } = string.Empty;

        [Display(Name = "Fordonstyp")]
        public VehicleType? VehicleType { get; set; } // = new VehicleType();

        [Display(Name = "Märke")]
        public string Brand { get; set; } = string.Empty;             

        [Display(Name = "Status")]
        public bool IsParked { get; set; }

        [Display(Name = "Ägare")]
        public Member? Member { get; set; } // = new Member();

     







    }
}
