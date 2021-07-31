using System;
using System.ComponentModel.DataAnnotations;

namespace ENSEKCoreWebApp.Models
{
    public class FormEditMeterReadingViewModel
    {
        [Display(Name = "Meter Reading Id")]
        public int Id { get; set; }

        [Display(Name = "Meter Reading Date Time")]
        public DateTime MeterReadingDateTime { get; set; }

        [Display(Name = "Meter Reading Value")]
        public int MeterReadingValue { get; set; }

        [Display(Name = "Account Id")]
        public int AccountId { get; set; }

    }
}
