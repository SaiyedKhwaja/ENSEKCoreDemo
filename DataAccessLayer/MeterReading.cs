using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer
{
    public class MeterReading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AccountId { get; set; }

        [DataType(DataType.Date)]
        public DateTime MeterReadingDateTime { get; set; }
        public int MeterReadingValue { get; set; }

        [NotMapped]
        public string MeterReadingStringValue { get; set; }
        [NotMapped]
        public bool IsSuccess { get; set; } = false;
        [NotMapped]
        public List<string> Messages { get; set; } = new List<string>();
    }
}
