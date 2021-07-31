using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLanguage.Models
{
    public class MeterReading
    {
        public string AccountId = "Account Id";
        public string Title = "Meter Readings";
        public string MeterReadingId = "Id";
        
        public string MeterReadingDateTime = "Meter Reading Data/Time";
        public string MeterReadingValue = "Meter Reading Value";
        public string UpdateDetails = "Update Meter Reading Details";

        private static MeterReading _instance = new MeterReading();
        public static MeterReading Instance
        {
            get { return _instance; }
        }
    }
}
