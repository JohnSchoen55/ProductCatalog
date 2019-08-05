using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Core.ExtensionMethods
{
    public static class DateTimeConversionExtension
    {
        public static DateTime ToCentralStandardTime(this DateTime date)
        {
            TimeZoneInfo cstTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(date.ToUniversalTime(), cstTimeZone);
        }
    }
}
