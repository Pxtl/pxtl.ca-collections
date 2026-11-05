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
}
