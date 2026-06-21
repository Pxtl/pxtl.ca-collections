using FluentAssertions;

namespace PxtlCa.Collections.Tests;

public class AutoConstructingDictTests
{
    [Fact]
    public void EmptyAutoConstructing()
    {
        var dict = new AutoConstructingDictionary<int, int>();
        (dict.Count).Should().Be(0);
    }
}
