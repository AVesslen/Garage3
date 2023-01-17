using System;
using System.Collections.Generic;
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
        public string RegNo { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        public DateTime ArrivalTime { get; set; }
        public bool IsParked { get; set; }

        
        public VehicleType VehicleType { get; set; } // Navigation properties
        public int VehicleTypeID { get; set; } // Foreign keys
        
        public Member Member { get; set; } // Navigation properties
        public int MemberID { get; set; } // FK


        public ICollection<Receipt> Receipts { get; set; } // Navigation properties
        //public Receipt Receipt { get; set; }

    }
}
