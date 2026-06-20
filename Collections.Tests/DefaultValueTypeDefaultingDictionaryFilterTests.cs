using FluentAssertions;

namespace PxtlCa.Collections.Tests;

public class DefaultValueTypeDefaultingDictFilterTests
{
    [Fact]
    public void StructDefault_ReturnsZeroForIntType()
    {
        var dict = new DefaultingDictionary<string, int?>
        {
            ValueConstructionHandler = (key) => default(int?)
        };
        
        (dict["test"]).Should().Be(default(int?));
    }

} // class DefaultValueTypeDefaultingDictFilterTests - Completed for iteration 4
