using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Garage3.Core
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Display(Name = "Regnummer"), StringLength(25, ErrorMessage = "Ange ett korrekt regnummer."), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string RegNo { get; set; } = string.Empty;

        [Display(Name = "Märke"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string Brand { get; set; } = string.Empty;

        [Display(Name = "Modell"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string VehicleModel { get; set; } = string.Empty;

        [Display(Name = "Färg"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string Color { get; set; } = string.Empty;

        [Display(Name = "Ankomsttid"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public DateTime ArrivalTime { get; set; }

        //[Display(Name = "Parkeringsstatus")]
        public bool IsParked { get; set; }

        // Navigation properties
        //[Display(Name = "Fordonstyp"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public VehicleType VehicleType { get; set; } // = new VehicleType();
        //[Display(Name = "Ägare")]
        public Member Member { get; set; } // = new Member();

        // Foreign keys
        [Display(Name = "Ägare")]
        public int MemberID { get; set; }
        public int VehicleTypeID { get; set; }


    }
}
