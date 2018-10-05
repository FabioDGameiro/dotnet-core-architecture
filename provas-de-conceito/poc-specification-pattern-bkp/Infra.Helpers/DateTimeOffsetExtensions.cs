#region Using

using System;

#endregion

namespace Infra.Helpers
{
    public static class DateTimeOffsetExtensions
    {
        public static int GetCurrentAge(this DateTime dateTimeOffset)
        {
            var currentDate = DateTime.UtcNow;
            var age = currentDate.Year - dateTimeOffset.Year;

            if (currentDate < dateTimeOffset.AddYears(age))
                age--;

            return age;
        }
    }
}