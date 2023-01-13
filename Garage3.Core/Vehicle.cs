using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Navigation properties
        public VehicleType VehicleType { get; set; } = new VehicleType();
        public Member Member { get; set; } = new Member();

        // Foreign keys
        public int MemberID { get; set; }
        public int VehicleTypeID { get; set; }


    }
}
