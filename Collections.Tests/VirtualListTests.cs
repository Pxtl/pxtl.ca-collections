using FluentAssertions;
using System.Collections.Generic;

namespace PxtlCa.Collections.Tests;

public class VirtualListTests
{
    [Fact]
    public void CreateEmptyVirtualList()
    {
        var list = new VirtualList<string>();
        (list.Count).Should().Be(0);
    }

    [Fact]
    public void WrapInBackingStore()
    {
        var backing = new List<string> { "a", "b" };
        var list = new VirtualList<string>(backing);

        (list.Count).Should().Be(2);
        (list[0]).Should().Be("a");
    }

    [Fact]
    public void EnumeratorGetsItemsFromBacking()
    {
        var source = new List<string> { "test" };
        var list = new VirtualList<string>(source);

        (source).Should().Contain("test");
    }
} // class VirtualListTests
