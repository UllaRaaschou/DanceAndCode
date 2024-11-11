using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

public class TimeValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true; // Return true if the value is null, as Required attribute should handle null checks
        }

        if (value is string timeString)
        {
            return TimeSpan.TryParseExact(timeString, "hh\\:mm", CultureInfo.InvariantCulture, out _);
        }

        return false;
    }
}

