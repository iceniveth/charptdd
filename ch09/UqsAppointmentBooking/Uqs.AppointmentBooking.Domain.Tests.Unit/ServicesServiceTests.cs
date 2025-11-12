using System;
using System.Linq;
using System.Threading.Tasks;
using Uqs.AppointmentBooking.Contract;
using Uqs.AppointmentBooking.Domain.Services;
using Uqs.AppointmentBooking.Domain.Tests.Unit.Fakes;
using Xunit;

namespace Uqs.AppointmentBooking.Domain.Tests.Unit;

public class ServicesServiceTests : IDisposable
{
    private readonly ApplicationContextFakeBuilder _ctxBlr = new();

    // private ServicesService? _sut;

    public void Dispose()
    {
        _ctxBlr.Dispose();
    }

    [Fact]
    public async Task GetActiveServices_NoServiceInTheSystem_NoServices()
    {
        var ctx = _ctxBlr.Build();
        var sut = new ServicesService(ctx);

        var actual = await sut.GetActiveServices();

        Assert.True(!actual.Any());
    }

    [Fact]
    public async Task GetActiveServices_TwoActiveOneInactiveService_TwoActiveServices()
    {
        var ctx = _ctxBlr
            .WithSingleService(true)
            .WithSingleService(true)
            .WithSingleService(false)
            .Build();
        var sut = new ServicesService(ctx);

        var actual = await sut.GetActiveServices();

        Assert.Equal(2, actual.LongCount());
    }
}
