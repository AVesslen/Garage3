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

        [Display(Name = "Förnamn"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string Brand { get; set; } = string.Empty;

        [Display(Name = "Modell"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string VehicleModel { get; set; } = string.Empty;

        [Display(Name = "Färg"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string Color { get; set; } = string.Empty;

        [Display(Name = "Ankomsttid")]
        public DateTime ArrivalTime { get; set; }

        //[Display(Name = "Parkeringsstatus")]
        public bool IsParked { get; set; }

        // Navigation properties
        public VehicleType VehicleType { get; set; }
        public Member Member { get; set; }

        // Foreign keys
        public int MemberID { get; set; } // FK
        public int VehicleTypeID { get; set; } // FK

        public ICollection<Receipt> Receipts { get; set; } // Navigation properties

    }
}
