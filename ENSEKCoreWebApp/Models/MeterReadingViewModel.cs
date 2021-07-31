using System.Collections.Generic;

namespace ENSEKCoreWebApp.Models
{
    public class MeterReadingViewModel
    {
        public int Id { get; set; }
        public List<MeterReading> meterReadings { get; set; } = new List<MeterReading>();
    }
}
