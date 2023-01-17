using Bogus.DataSets;
using Garage3.Core;
using System.ComponentModel.DataAnnotations;

namespace Garage3.ViewModels
{
    public class VehicleCreateViewModel
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

        //[Display(Name = "Ankomsttid")]
        //public DateTime ArrivalTime { get; set; }

        [Display(Name = "Parkera direkt")]
        public bool IsParked { get; set; }

        [Display(Name = "Fordonstyp")]
        //public VehicleType? VehicleType { get; set; } // = new VehicleType();
        public int VehicleTypeId { get; set; } 

        [Display(Name = "Ägare")]
        public int MemberId { get; set; } // = new Member();



    }
}
