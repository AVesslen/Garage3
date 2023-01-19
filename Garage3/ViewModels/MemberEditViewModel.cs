using Bogus.DataSets;
using System.ComponentModel.DataAnnotations;

namespace Garage3.ViewModels
{
    public class MemberEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Förnamn"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string FirstName { get; set; } = string.Empty;


        [Display(Name = "Efternamn"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string LastName { get; set; } = string.Empty;


        [Display(Name = "Personnummer"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string PersonalNo { get; set; } = string.Empty;
    }
}
