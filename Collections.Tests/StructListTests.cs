namespace PxtlCa.Collections.Tests;

public class StructListTests {
    [Fact]
    public void EmptyStructList_CanEnumerate() {
        var structList = new StructList<int>();
        var hasRun = false;
        foreach (var myInt in structList) {
            hasRun = true;
        }
        hasRun.Should().BeFalse();
    }

    [Fact]
    public void EmptyStructList_CanGetCount() {
        var structList = new StructList<int>();
        structList.Count.Should().Be(0);
    }

    [Fact]
    public void EmptyStructList_CanCallContains() {
        var structList = new StructList<int>();
        structList.Contains(999).Should().BeFalse();
    }

    [Fact]
    public void EmptyStructList_CanCallIndexOf() {
        var structList = new StructList<int>();
        structList.IndexOf(999).Should().Be(-1);
    }

    [Fact]
    public void StructList_ConstructsCorrectly() {
        var structList = new StructList<int>([1, 2, 3]);
        structList.Should().BeEquivalentTo([1, 2, 3]);
    }

    [Fact]
    public void StructList_IndexOfReturnsCorrectIndex() {
        var structList = new StructList<string>(["one", "two", "three"]);
        structList.IndexOf("two").Should().Be(1);
    }

    [Fact]
    public void StructList_ContainsReturnsTrueWhenExists() {
        var structList = new StructList<string>(["one", "two", "three"]);
        structList.Contains("two").Should().BeTrue();
    }

    [Fact]
    public void StructList_ContainsReturnsFalseWhenNotExist() {
        var structList = new StructList<string>(["one", "two", "three"]);
        structList.Contains("zero").Should().BeFalse();
    }

    [Fact]
    public void StructList_IndexerReturnsCorrectValue() {
        var structList = new StructList<string>(["one", "two", "three"]);
        structList[1].Should().Be("two");
    }

    [Fact]
    public void StructListOperators_SameDataIsEqual() {
        var structList1 = new StructList<string>(["one", "two", "three"]);
        var structList2 = new StructList<string>(["one", "two", "three"]);
        (structList1 == structList2).Should().BeTrue();
        (structList1 != structList2).Should().BeFalse();
        structList1.Equals(structList2).Should().BeTrue();
    }

    [Fact]
    public void StructListOperators_PlusConcatenates() {
        var structList1 = new StructList<string>(["one", "two", "three"]);
        var structList2 = new StructList<string>(["four", "five", "six"]);
        (structList1 + structList2).Should().BeEquivalentTo(["one", "two", "three", "four", "five", "six"]);
        (structList2 + structList1).Should().BeEquivalentTo(["four", "five", "six", "one", "two", "three"]);
    }
}