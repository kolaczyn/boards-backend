using boards.Domain.Providers;

namespace boards.Infrastructure.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now()
    {
        return DateTime.UtcNow;
    }
}