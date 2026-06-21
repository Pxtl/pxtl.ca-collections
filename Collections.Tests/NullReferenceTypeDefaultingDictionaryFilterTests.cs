using FluentAssertions;

namespace PxtlCa.Collections.Tests;

public class NullReferenceTypeDefaultingDictFiltersTests
{
    [Fact]
    public void ReferenceTypeNull_GetNull()
    {
        var dict = new DefaultingDictionary<string, string?>
        {
            ValueConstructionHandler = (key) => default(string?)
        };
        
        var val = dict["test"];
        (val).Should().BeNull();
    }

}
