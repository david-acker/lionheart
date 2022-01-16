using Lionheart.Core.Helpers;
using Xunit;

namespace Lionheart.Core.Test.Helpers;

public class AccessKeyHelperTests
{
    [Fact]
    public void Generate_NewAccessKey_ShouldBeAllUppercase()
    {
        // Act
        string actual = AccessKeyHelper.Generate();

        // Assert
        Assert.True(actual == actual.ToUpper());
    }

    [Fact]
    public void Generate_NewAccessKey_ShouldBeAlphaNumeric()
    {
        // Act
        string actual = AccessKeyHelper.Generate();

        // Assert
        Assert.Matches("[A-Z0-9]+", actual);
    }

    [Fact]
    public void Generate_NewAccessKey_ShouldBeExpectedLength()
    {
        // Act
        string actual = AccessKeyHelper.Generate();

        // Assert
        Assert.True(actual.Length == 32);
    }
}
