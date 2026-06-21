using FluentAssertions;

namespace PxtlCa.Collections.Tests;

public class DefaultingDictTests
{
    [Fact]
    public void EmptyDefaultingDict()
    {
        var dict = new DefaultingDictionary<int, string?>();
        dict.Should().BeEmpty();
    }

    [Fact]
    public void AddThenRetrieve_ExistingValue()
    {
        var dict = new DefaultingDictionary<string, int?>
        {
            ["two"] = 2,
            ValueConstructionHandler = (key) => -99
        };

        dict["two"].Should().Be(2);
    }

    [Fact]
    public void MissingKey_GetDefaultFromHandler()
    {
        var dict = new DefaultingDictionary<string, int?>
        {
            ["one"] = 1,
            ValueConstructionHandler = (key) => -99
        };

        dict["four"].Should().Be(-99);
    }

    [Fact]
    public void SetThenGetStoredKey()
    {
        var dict = new DefaultingDictionary<string, string?>
        {
            ValueConstructionHandler = (key) => null
        };

        dict["test"] = "provided";
        dict["test"].Should().Be("provided");
    }

    [Fact]
    public void GetMissingKey_ReturnsNullForNullableRefType()
    {
        var dict = new DefaultingDictionary<string, string?>
        {
            ValueConstructionHandler = (key) => null
        };

        dict["other"].Should().BeNull();
    }
}
