using Bogus.DataSets;
using Garage3.Core;
using System.ComponentModel.DataAnnotations;

namespace Garage3.ViewModels
{
    public class VehicleDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Registreringsnummer")]
        public string RegNo { get; set; } = string.Empty;

        [Display(Name = "Märke")]
        public string Brand { get; set; } = string.Empty;

        [Display(Name = "Modell")]
        public string VehicleModel { get; set; } = string.Empty;

        [Display(Name = "Färg")]
        public string Color { get; set; } = string.Empty;

        [Display(Name = "Ankomsttid")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Status")]
        public bool IsParked { get; set; }

        [Display(Name = "Fordonstyp")]
        public VehicleType? VehicleType { get; set; } // = new VehicleType();

        [Display(Name = "Ägare")]
        public Member? Member { get; set; } // = new Member();

    }
}
