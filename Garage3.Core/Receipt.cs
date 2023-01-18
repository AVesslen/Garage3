using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

#nullable disable

namespace Garage3.Core
{
    public class Receipt
    {
        public int Id { get; set; }

        [Display(Name = "Regnummer")]
        public string RegNo { get; set; } = string.Empty;

        [Display(Name = "Fordonstyp")]
        public VehicleType VehicleType { get; set; }

        [Display(Name = "Ankomsttid")]
        public DateTime TimeEnter { get; set; }

        [Display(Name = "Utcheckningstid")]
        public DateTime TimeExit { get; set; }

        [Display(Name = "Timpris (kr)")]
        public int Price { get; set; }

        [Display(Name = "Pris total (kr)")]
        public int PriceTotal { get; set; }

       
        public Member Member { get; set; } // Navigation property
        public int MemberId { get; set; } //FK


        public Vehicle Vehicle { get; set; } // Navigation property
        public int VehicleId { get; set; } //FK

    }
}
