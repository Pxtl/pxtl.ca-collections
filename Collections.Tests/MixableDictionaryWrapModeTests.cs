using FluentAssertions;

namespace PxtlCa.Collections.Tests;

/// <summary> Tests for MixableDictionary static Wrap functionality </summary>
public class MixableDictWrapModeTests
{
    [Fact]
    public void WrapCopiesExistingData()
    {
        var original = new System.Collections.Generic.Dictionary<string, int?> { ["key"] = 10 };

        var wrapped = MixableDictFactory.Wrap<string, int?>(original);
        (wrapped.ContainsKey("key")).Should().BeTrue();
    }

} // class MixableDictWrapModeTests
