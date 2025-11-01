namespace Uqs.Arithmetic.Tests.Unit;

public class DivisionTests
{
    // MethodName_Condition_Expectation
    [Fact]
    public void Divide_DivisibleIntegers_WholeNumber()
    {
        int dividend = 10;
        int divisor = 5;
        decimal expectedQuotient = 2;

        decimal actualQuotient = Division.Divide(dividend, divisor);

        Assert.Equal(expectedQuotient, actualQuotient);
    }

    [Fact]
    public void Divide_IndivisbleIntegers_DecimalNumber()
    {
        int dividend = 10;
        int divisor = 4;
        decimal expectedQuotient = 2.5m;

        decimal actualQuotient = Division.Divide(dividend, divisor);

        Assert.Equal(expectedQuotient, actualQuotient);
    }

    [Fact]
    public void Divide_ZeroDivisor_DivideByZeroException()
    {
        int dividend = 10;
        int divisor = 0;

        Exception e = Record.Exception(() => Division.Divide(dividend, divisor));

        Assert.IsType<DivideByZeroException>(e);
    }

    [Fact]
    public void Divide_IndivisibleIntegers_DecimalNumber()
    {
        int dividend = 10;
        int divisor = 4;
        decimal expectedQuotient = 2.5m;

        decimal actualQuotient = Division.Divide(dividend, divisor);

        Assert.Equal(expectedQuotient, actualQuotient);
    }

    [Theory]
    [InlineData(int.MaxValue, int.MinValue, -0.999999999534)]
    [InlineData(-int.MaxValue, int.MinValue, 0.999999999534)]
    [InlineData(int.MinValue, int.MaxValue, -1.000000000466)]
    [InlineData(int.MinValue, -int.MaxValue, 1.000000000466)]
    public void Divide_ExtremeInput_CorrectCalculation(int dividend, int divisor, decimal expectedQuotient)
    {
        decimal actualQuotient = Division.Divide(dividend, divisor);

        Assert.Equal(expectedQuotient, actualQuotient, 12);
    }
}