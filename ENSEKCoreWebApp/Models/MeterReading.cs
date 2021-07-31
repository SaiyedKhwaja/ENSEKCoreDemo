using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ENSEKCoreWebApp.Models
{
    public class MeterReading
    {
        [Display(Name = "Meter Reading Id")]
        public int Id { get; set; }
        public int AccountId { get; set; }
        [Display(Name = "Meter Reading Date Time")]
        public DateTime MeterReadingDateTime { get; set; }

        [Display(Name = "Meter Reading Value")]
        public int MeterReadingValue { get; set; }

        public string MeterReadingStringValue { get; set; }
        
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; }
        public virtual Account Account { get; set; }
    }
}
