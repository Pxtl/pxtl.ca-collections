using FluentAssertions;

namespace PxtlCa.Collections.Tests;

public class AutoConstructingDictTests
{
    [Fact]
    public void EmptyAutoConstructing()
    {
        var dict = new AutoConstructingDictionary<int, int>();
    }

} // class AutoConstructingDictTests
