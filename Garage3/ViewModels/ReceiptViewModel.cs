using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Garage3.Core;
using System.ComponentModel.DataAnnotations;
using Vehicle = Garage3.Core.Vehicle;

#nullable disable

namespace Garage3.Core
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Regnummer"), StringLength(15)]
        public string RegNo { get; set; } = string.Empty;

        [Display(Name = "Fordonstyp")]
        public VehicleType VehicleType { get; set; }

        [Display(Name = "Ankomsttid")]
        public DateTime TimeEnter { get; set; }

        [Display(Name = "Utcheckning")]
        public DateTime TimeExit { get; set; }

        [Display(Name = "Timpris (kr)")]
        public int Price { get; set; }

        [Display(Name = "Att betala (kr)")]
        public int PriceTotal { get; set; }
    }
}
