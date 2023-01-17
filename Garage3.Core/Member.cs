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


        public ICollection<Vehicle> Vehicles { get; set; } // Navigation properties

        public ICollection<Receipt> Receipts { get; set; } // Navigation properties
        //public Receipt Receipt { get; set; }

    }
}
