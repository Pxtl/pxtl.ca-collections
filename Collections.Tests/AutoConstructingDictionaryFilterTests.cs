using FluentAssertions;

namespace PxtlCa.Collections.Tests;

/// <summary> Tests covering the AutoConstructingDictionaryFilter auto-construction behavior using int types for simpler testing </summary>
public class AutoConstructingDictFilterBehaviorTests
{
    [Fact]
    public void MissingKeyAutoConstructsWithIntKey()
    {
        var dict = new PxtlCa.Collections.AutoConstructingDictionary<int, int>();
        
        // Access missing key triggers auto-construction (zero for int)  
        // Using int key 0 to satisfy TKey constraint
        _ = dict[0];

        dict[0].Should().Be(0);
    }
}
