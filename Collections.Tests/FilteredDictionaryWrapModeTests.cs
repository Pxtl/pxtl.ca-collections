using FluentAssertions;

namespace PxtlCa.Collections.Tests;

/// <summary> Tests for FilteredDictionary static Wrap functionality </summary>
public class FilteredDictWrapModeTests {
    [Fact]
    public void WrapCopiesExistingData() {
        var original = new Dictionary<string, int?> { ["key"] = 10 };

        var wrapped = FilteredDictionaryFactory.Wrap(original);
        wrapped.ContainsKey("key").Should().BeTrue();
    }

}
