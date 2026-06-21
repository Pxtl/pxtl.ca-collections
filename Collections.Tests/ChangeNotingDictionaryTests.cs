using FluentAssertions;
using System.Collections.Generic;

namespace PxtlCa.Collections.Tests;

public class ChangeNotingDictTests
{
    [Fact]
    public void NoteChangesDisabled()
    {
        var dict = new ChangeNotingDictionary<string, string>
        {
            NoteChanges = false
        };
        (dict.Count).Should().Be(0);
    }
}
