using ENSEKCoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENSEKCoreWebApp.Helpers
{
    public class DistinctItemComparer : IEqualityComparer<MeterReading>
    {

        public bool Equals(MeterReading x, MeterReading y)
        {
            return x.AccountId == y.AccountId &&
                x.MeterReadingDateTime.ToShortDateString() == y.MeterReadingDateTime.ToShortDateString() &&
                x.MeterReadingStringValue == y.MeterReadingStringValue;
        }

        public int GetHashCode(MeterReading obj)
        {
            return obj.AccountId.GetHashCode() ^
                obj.MeterReadingDateTime.GetHashCode() ^
                obj.MeterReadingStringValue.GetHashCode();
        }
    }
}
