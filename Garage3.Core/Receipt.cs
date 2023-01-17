using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Garage3.Core
{
    public class Receipt
    {
        public int Id { get; set; }
        public string RegNo { get; set; } = string.Empty;
        public VehicleType VehicleType { get; set; }
        public DateTime TimeEnter { get; set; }
        public DateTime TimeExit { get; set; }
        public int Price { get; set; }
        public int PriceTotal { get; set; }

        
        public Member Member { get; set; } // Navigation properties
        public int MemberId { get; set; } //FK


        public Vehicle Vehicle { get; set; } // Navigation properties
        public int VehicleId { get; set; } //FK

    }
}
