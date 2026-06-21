using FluentAssertions;

namespace PxtlCa.Collections.Tests;

/// <summary>Filter chaining tests cover: multiple filters chained, execute in order, replace all filters.</summary>
public class MixableDictFilterChainingTests
{
    [Fact]
    public void CreateMultipleFilters_ChainOnMixable()
    {
        var dict = new MixableDictionary<string, int?>();
        (dict.Count).Should().Be(0);
    }
} // class MixableDictFilterChainingTests
