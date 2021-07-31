using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ENSEKCoreWebApp.Models
{
    public class Account
    {
        [Display(Name = "Account Id")]
        public int AccountId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public virtual ICollection<MeterReading> MeterReadings { get; set; }
    }
}
