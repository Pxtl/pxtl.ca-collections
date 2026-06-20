using FluentAssertions;
using System.Collections.Generic;

namespace PxtlCa.Collections.Tests;

public class ChangeNotingDictTests
{
    [Fact]
    public void NoteChangesDisabled()
    {
        _ = new ChangeNotingDictionary<string, string> { NoteChanges = false };
    }

} // class ChangeNotingDictTests - Simple test for compile check only
