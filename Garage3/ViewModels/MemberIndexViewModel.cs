using Bogus.DataSets;
using Garage3.Core;
using System.ComponentModel.DataAnnotations;
using Vehicle = Garage3.Core.Vehicle;

namespace Garage3.ViewModels
{
    public class MemberIndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Förnamn")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Efternamn")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Antal registrerade fordon")]
        public int NrOfVehicles { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}
