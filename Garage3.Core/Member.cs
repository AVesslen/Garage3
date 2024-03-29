﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Garage3.Core
{
    public class Member
    {
        public int Id { get; set; }


        [Display(Name = "Förnamn"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Efternamn"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "Personnummer"), Required(ErrorMessage = "Detta fält måste fyllas i.")]
        public string PersonalNo { get; set; } = string.Empty;

        public ICollection<Vehicle> Vehicles { get; set; } // Navigation properties

        public ICollection<Receipt> Receipts { get; set; } // Navigation properties

    }
}
