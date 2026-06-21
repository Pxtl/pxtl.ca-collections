using FluentAssertions;

namespace PxtlCa.Collections.Tests;

public class DefaultFiltersTests {
    [Fact]
    public void DelegateHandler_ConstructsMissingValue() {
        var dict = new DefaultingDictionary<string, string?> {
            ValueConstructionHandler = (key) => $"default_for_{key}"
        };
        (dict["test"]).Should().Contain("test");
    }

}
