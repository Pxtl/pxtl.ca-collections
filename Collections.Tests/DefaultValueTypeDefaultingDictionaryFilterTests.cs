using FluentAssertions;

namespace PxtlCa.Collections.Tests;

public class DefaultValueTypeDefaultingDictFilterTests {
    [Fact]
    public void StructDefault_ReturnsNullForNullableIntType() {
        var dict = new DefaultingDictionary<string, int?> {
            ValueConstructionHandler = (key) => default(int?)
        };

        (dict["test"]).Should().Be(default(int?));
    }

}
