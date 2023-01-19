using Bogus.DataSets;
using Garage3.Core;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Garage3.ViewModels
{
    public class VehicleCreateViewModel
    {

        public int Id { get; set; }
        [Remote(action: "CheckIfRegNoIsUnique", controller: "Vehicles")]
        [Display(Name = "Registreringsnummer"), Required(ErrorMessage ="Detta fält måste fyllas i.")]
        public string RegNo { get; set; } = string.Empty;

        [Display(Name = "Märke"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string Brand { get; set; } = string.Empty;

        [Display(Name = "Modell"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string VehicleModel { get; set; } = string.Empty;

        [Display(Name = "Färg"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string Color { get; set; } = string.Empty;
        
        [Display(Name = "Parkera direkt")]
        public bool IsParked { get; set; }

        [Display(Name = "Fordonstyp")]
        //public VehicleType? VehicleType { get; set; } // = new VehicleType();
        public int VehicleTypeId { get; set; } 

        [Display(Name = "Ägare")]
        public int MemberId { get; set; } // = new Member();



    }
}
