using FluentAssertions;

namespace PxtlCa.Collections.Tests;

public class ConcurrentHashSetTests {
    [Fact]
    public void EmptySet_HasNoElements() {
        var set = new ConcurrentHashSet<int>();
        set.Count.Should().Be(0);
        set.Should().BeEmpty();
    }

    [Fact]
    public void Add_ReturnsTrue_WhenElementIsNew() {
        var set = new ConcurrentHashSet<int>();
        set.Add(1).Should().BeTrue();
    }

    [Fact]
    public void Add_ReturnsFalse_WhenElementAlreadyPresent() {
        var set = new ConcurrentHashSet<int>();
        set.Add(1).Should().BeTrue();
        set.Add(1).Should().BeFalse();
    }

    [Fact]
    public void Count_ReflectsNumberOfDistinctElements() {
        var set = new ConcurrentHashSet<int>();
        set.Add(1);
        set.Add(2);
        set.Add(1);
        set.Count.Should().Be(2);
    }

    [Fact]
    public void Contains_ReportsMembership() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Contains("a").Should().BeTrue();
        set.Contains("b").Should().BeFalse();
    }

    [Fact]
    public void Remove_ReturnsTrue_WhenElementPresent() {
        var set = new ConcurrentHashSet<int>();
        set.Add(5);
        set.Remove(5).Should().BeTrue();
        set.Contains(5).Should().BeFalse();
    }

    [Fact]
    public void Remove_ReturnsFalse_WhenElementAbsent() {
        var set = new ConcurrentHashSet<int>();
        set.Remove(99).Should().BeFalse();
    }

    [Fact]
    public void Clear_RemovesAllElements() {
        var set = new ConcurrentHashSet<int>();
        set.Add(1);
        set.Add(2);
        set.Clear();
        set.Count.Should().Be(0);
        set.Should().BeEmpty();
    }

    [Fact]
    public void Enumeration_ReturnsAllDistinctElements() {
        var set = new ConcurrentHashSet<int>();
        set.Add(1);
        set.Add(2);
        set.Add(3);
        set.Should().BeEquivalentTo(new[] { 1, 2, 3 });
    }

    [Fact]
    public async Task ConcurrentAdd_MaintainsUniqueness() {
        var set = new ConcurrentHashSet<int>();
        var tasks = new List<Task>(50);
        for (var t = 0; t < 50; t++) {
            tasks.Add(Task.Run(() => {
                for (var v = 0; v < 50; v++) { set.Add(v); }
            }, TestContext.Current.CancellationToken));
        }
        await Task.WhenAll(tasks);
        set.Count.Should().Be(50);
    }

    [Fact]
    public void ISet_UnionWith_AddsOnlyMissing() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        ((ISet<string>)set).UnionWith(new[] { "b", "c", "d" });
        set.Should().BeEquivalentTo("a", "b", "c", "d");
    }

    [Fact]
    public void ISet_ExceptWith_RemovesShared() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        set.Add("c");
        ((ISet<string>)set).ExceptWith(new[] { "b", "z" });
        set.Should().BeEquivalentTo("a", "c");
    }

    [Fact]
    public void ISet_IntersectWith_KeepsOnlyShared() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        set.Add("c");
        ((ISet<string>)set).IntersectWith(new[] { "b", "d" });
        set.Should().BeEquivalentTo("b");
    }

    [Fact]
    public void ISet_SymmetricExceptWith_TogglesMembership() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        set.Add("c");
        ((ISet<string>)set).SymmetricExceptWith(new[] { "b", "d" });
        set.Should().BeEquivalentTo("a", "c", "d");
    }

    [Fact]
    public void ISet_IsProperSubsetOf_TrueWhenStrictlySmaller() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        ((ISet<string>)set).IsProperSubsetOf(new[] { "a", "b", "c" }).Should().BeTrue();
    }

    [Fact]
    public void ISet_IsProperSubsetOf_FalseWhenEqual() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        ((ISet<string>)set).IsProperSubsetOf(new[] { "a", "b" }).Should().BeFalse();
    }

    [Fact]
    public void ISet_IsProperSubsetOf_FalseWhenNotSubset() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        ((ISet<string>)set).IsProperSubsetOf(new[] { "b", "c" }).Should().BeFalse();
    }

    [Fact]
    public void ISet_SetEquals_TrueWhenSameElements() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        ((ISet<string>)set).SetEquals(new[] { "b", "a" }).Should().BeTrue();
    }

    [Fact]
    public void ISet_SetEquals_FalseWhenDifferent() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        ((ISet<string>)set).SetEquals(new[] { "a", "c" }).Should().BeFalse();
        ((ISet<string>)set).SetEquals(new[] { "a" }).Should().BeFalse();
    }

    [Fact]
    public void ICollection_CopyTo_CopiesAllElements() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        var array = new string[2];
        ((ICollection<string>)set).CopyTo(array, 0);
        array.Should().BeEquivalentTo("a", "b");
    }

    [Fact]
    public void ICollection_IsReadOnly_IsFalse() {
        var set = new ConcurrentHashSet<string>();
        ((ICollection<string>)set).IsReadOnly.Should().BeFalse();
    }

    [Fact]
    public void ISet_Overlaps_TrueWhenShared() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        ((ISet<string>)set).Overlaps(new[] { "x", "a" }).Should().BeTrue();
    }

    [Fact]
    public void ISet_Overlaps_FalseWhenDisjoint() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        ((ISet<string>)set).Overlaps(new[] { "x", "y" }).Should().BeFalse();
    }

    [Fact]
    public void ISet_IsSupersetOf_TrueWhenContainsAll() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        ((ISet<string>)set).IsSupersetOf(new[] { "a", "b" }).Should().BeTrue();
    }

    [Fact]
    public void ISet_IsSupersetOf_FalseWhenMissingElement() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        ((ISet<string>)set).IsSupersetOf(new[] { "a", "b" }).Should().BeFalse();
    }

    [Fact]
    public void ISet_IsProperSupersetOf_TrueWhenStrictlyLarger() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        set.Add("c");
        ((ISet<string>)set).IsProperSupersetOf(new[] { "a", "b" }).Should().BeTrue();
    }

    [Fact]
    public void ISet_IsProperSupersetOf_FalseWhenEqual() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        ((ISet<string>)set).IsProperSupersetOf(new[] { "a", "b" }).Should().BeFalse();
    }

    [Fact]
    public void ISet_IsProperSupersetOf_FalseWhenNotSuperset() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        ((ISet<string>)set).IsProperSupersetOf(new[] { "b", "c" }).Should().BeFalse();
    }

    [Fact]
    public void ISet_IsSubsetOf_TrueWhenContained() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        ((ISet<string>)set).IsSubsetOf(new[] { "a", "b", "c" }).Should().BeTrue();
    }

    [Fact]
    public void ISet_IsSubsetOf_FalseWhenNotContained() {
        var set = new ConcurrentHashSet<string>();
        set.Add("a");
        set.Add("b");
        ((ISet<string>)set).IsSubsetOf(new[] { "a", "c" }).Should().BeFalse();
    }
}
