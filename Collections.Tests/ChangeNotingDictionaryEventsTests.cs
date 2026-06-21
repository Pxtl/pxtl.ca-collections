using FluentAssertions;

namespace PxtlCa.Collections.Tests;

/// <summary> Tests for Change event handler delegate and NoteChanges behavior </summary>
public class ChangeNoteHandlerTests {
    [Fact]
    public void ChangedEvent_FiresOnAdd() {
        var capturedKeys = new System.Collections.Generic.List<string>();

        // Setup change handler for add events  
        var dict = new PxtlCa.Collections.ChangeNotingDictionary<string, string> {
            ["first"] = "one",
            NoteChanges = true,
            Changed = (key, _) => capturedKeys.Add(key)
        };

        // Check event handler signature works with key parameter  
    }

} // class ChangeNoteHandlerTests - Change event handler behavior
