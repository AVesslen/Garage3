﻿using Bogus.DataSets;
using Garage3.Core;
using System.ComponentModel.DataAnnotations;

namespace Garage3.ViewModels
{
    public class VehicleIndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Registreringsnummer")]
        public string RegNo { get; set; } = string.Empty;

        [Display(Name = "Fordonstyp")]
        public VehicleType? VehicleType { get; set; } // = new VehicleType();

        [Display(Name = "Märke")]
        public string Brand { get; set; } = string.Empty;             

        [Display(Name = "Parkeringsstatus")]
        public bool IsParked { get; set; }

        [Display(Name = "Ägare")]
        public Member? Member { get; set; } // = new Member();

        [Display(Name = "Ankomsttid")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Parkerad tid")]
        [DisplayFormat(DataFormatString = "{0:%h}t {0:%m}m")]  //  [DisplayFormat(DataFormatString = "{0:%d}d {0:%h}t {0:%m}m")]
        public TimeSpan ParkedTime { get; set; }









    }
}
