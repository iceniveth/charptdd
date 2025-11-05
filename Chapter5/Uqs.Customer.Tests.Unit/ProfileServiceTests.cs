namespace Uqs.Customer.Tests.Unit;

public class ProfileServiceTests
{
    [Fact]
    public void ChangeUsername_NullUsername_ArgumentNullException()
    {
        var sut = new ProfileService();

        var e = Record.Exception(() => sut.ChangeUsername(null));

        var ex = Assert.IsType<ArgumentNullException>(e);
        Assert.Equal("username", ex.ParamName);
        Assert.StartsWith("Null", ex.Message);
    }

    [Theory]
    [InlineData("AnameOf8", true)]
    [InlineData("NameOfChar12", true)]
    [InlineData("AnameOfChar13", false)]
    [InlineData("NameOf7", false)]
    [InlineData("", false)]
    public void ChangeUsername_VariousLengthUsernames_ArgumentOutOfRangeException(
        string username,
        bool isValid
    )
    {
        var sut = new ProfileService();

        var e = Record.Exception(() => sut.ChangeUsername(username));

        if (isValid)
        {
            Assert.Null(e);
        }
        else
        {
            var ex = Assert.IsType<ArgumentOutOfRangeException>(e);
            Assert.Equal("username", ex.ParamName);
            Assert.StartsWith("Length", ex.Message);
        }
    }

    [Theory]
    [InlineData("Letter_123", true)]
    [InlineData("!The_Start", false)]
    [InlineData("InThe@Middle", false)]
    [InlineData("WithDollar$", false)]
    [InlineData("Space 123", false)]
    public void ChangeUsename_InvalidCharacterValidation_ArgumentOutOfRangeException(
        string username,
        bool isValid
    )
    {
        var sut = new ProfileService();

        var e = Record.Exception(() => sut.ChangeUsername(username));

        if (isValid)
        {
            Assert.Null(e);
        }
        else
        {
            var ex = Assert.IsType<ArgumentOutOfRangeException>(e);
            Assert.Equal("username", ex.ParamName);
            // Assert.Equal("InvalidChar", ex.Message);
        }
    }
}
