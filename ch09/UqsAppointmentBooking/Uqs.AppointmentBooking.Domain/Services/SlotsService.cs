using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Uqs.AppointmentBooking.Domain.Database;
using Uqs.AppointmentBooking.Domain.DomainObjects;
using Uqs.AppointmentBooking.Domain.Report;

namespace Uqs.AppointmentBooking.Domain.Services;

public interface ISlotsService
{
    Task<Slots> GetAvailableSlotsForEmployee(int serviceId);
}

public class SlotsService : ISlotsService
{
    private readonly ApplicationContext _context;
    private readonly ApplicationSettings _settings;
    private readonly DateTime _now;
    private readonly TimeSpan _roundingIntervalSpan;

    public SlotsService(
        ApplicationContext context,
        INowService nowService,
        IOptions<ApplicationSettings> settings
    )
    {
        _context = context;
        _settings = settings.Value;
        _roundingIntervalSpan = TimeSpan.FromMinutes(_settings.RoundUpInMin);
        _now = RoundUpToNearest(nowService.Now);
    }

    public async Task<Slots> GetAvailableSlotsForEmployee(int serviceId)
    {
        var service = await _context.Services!.SingleOrDefaultAsync(s =>
            s.IsActive && s.Id == serviceId
        );

        if (service is null)
        {
            throw new ArgumentException("Record not found");
        }

        return null;
    }

    private DateTime GetEndOfOpenAppointments() =>
        _now.Date.AddDays(_settings.OpenAppointmentInDays);

    private DateTime RoundUpToNearest(DateTime dt)
    {
        var ticksInSpan = _roundingIntervalSpan.Ticks;
        return new DateTime((dt.Ticks + ticksInSpan - 1) / ticksInSpan * ticksInSpan, dt.Kind);
    }

    private bool IsPeriodIntersecting(
        DateTime fromT1,
        DateTime toT1,
        DateTime fromT2,
        DateTime toT2
    ) => fromT1 < toT2 && toT1 > fromT2;
}
