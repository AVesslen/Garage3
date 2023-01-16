using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Garage3.Core
{
    public class Member
    {
        public int Id { get; set; }

        public string MemberNo { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PersonalNo { get; set; } = string.Empty;


        // Navigation properties

        public ICollection<Vehicle> Vehicles { get; set; } // = new List<Vehicle>();
    }
}
