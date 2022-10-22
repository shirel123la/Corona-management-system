//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HospitalSystem_Corona.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Vaccination
    {
        [Display(Name = "vaccination id")]

        public int vaccination_id { get; set; }


        [Display(Name = "patient id ")]
        public Nullable<int> patient_id { get; set; }


        [Display(Name = "manufacturer id")]
        public Nullable<int> manufacturer_id { get; set; }

        [Display(Name = "vaccination date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> vaccination_date { get; set; }

        internal static string ToList()
        {
            throw new NotImplementedException();
        }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual Patient Patient { get; set; }
    }
}