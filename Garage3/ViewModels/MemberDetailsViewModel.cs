using Bogus.DataSets;
using Garage3.Core;
using System.ComponentModel.DataAnnotations;
using Vehicle = Garage3.Core.Vehicle;

#nullable disable

namespace Garage3.ViewModels
{
    public class MemberDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Förnamn")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Efternamn")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Personnummer")]
        public string PersonalNo { get; set; } = string.Empty;

        [Display(Name = "Antal registrerade fordon")]
        public int NrOfVehicles { get; set; }

        [Display(Name = "Parkeringsstatus")]
        public string ParkingStatus { get; set; } = string.Empty;

        [Display(Name = "Fordon")]
        public ICollection<Vehicle> Vehicles { get; set; } //= new List<Vehicle>();
        public VehicleType VehicleType { get; set; } // = new VehicleType();


    }
}
