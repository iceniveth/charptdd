using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSubstitute;
using Uqs.AppointmentBooking.Domain.Services;
using Uqs.AppointmentBooking.Domain.Tests.Unit.Fakes;
using Xunit;

namespace Uqs.AppointmentBooking.Domain.Tests.Unit;

public class SlotsServiceTests : IDisposable
{
    private readonly ApplicationContextFakeBuilder _ctxBlr = new();
    private readonly INowService _nowService = Substitute.For<INowService>();
    private readonly ApplicationSettings _applicationSettings = new()
    {
        OpenAppointmentInDays = 7,
        RoundUpInMin = 5,
        RestInMin = 5,
    };
    private readonly IOptions<ApplicationSettings> _settings = Substitute.For<
        IOptions<ApplicationSettings>
    >();

    public SlotsServiceTests()
    {
        _settings.Value.Returns(_applicationSettings);
    }

    public void Dispose()
    {
        _ctxBlr.Dispose();
    }

    [Fact]
    public async Task GetAvailableSlotsForEmployee_ServiceIdNotFound_ArgumentException()
    {
        var ctx = _ctxBlr.Build();
        var sut = new SlotsService(ctx, _nowService, _settings);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            sut.GetAvailableSlotsForEmployee(-1)
        );

        Assert.IsType<ArgumentException>(exception);
    }
}
