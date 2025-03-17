using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ecommerce.Application;

public class DecimalToDouble : ValueConverter<decimal, double>
{
    public DecimalToDouble() : base(dec => decimal.ToDouble(dec), doub => (decimal)doub)
    {
    }
}

public class DateTimeToDateTimeUtc : ValueConverter<DateTime, DateTime>
{
    public DateTimeToDateTimeUtc() : base(c => DateTime.SpecifyKind(c, DateTimeKind.Utc), c => c)
    {
    }
}
