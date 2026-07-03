namespace PxtlCa.Collections.Proxies;

public class RefProxy<T> : ValueProxy<T> {
    public RefProxy(T val) : base(val) { }

    /// <summary>
    /// Allows you to simulate RAII when wrapping a ref variable in a <see
    /// cref="RefProxy{T}"/> by using a lambda.
    /// </summary>
    /// <param name="value">The ref value to wrap in a <see cref="RefProxy{T}"/>
    /// proxy.</param>
    /// <param name="action">The action that will manipulate the <see cref="RefProxy{T}"/>.</param>
    /// <example>
    /// <code>
    /// var myString = "Before";
    /// foreach(var proxy in Proxy.AsPseudoEnumerable(ref myString)) {
    ///     proxy.Value = "After";
    /// }
    /// myString.Should().Be("After");
    /// </code>
    /// </example>
    public static void UsingVal(ref T value, Action<RefProxy<T>> action) {
        RefProxy<T> proxy = new RefProxy<T>(value);
        action(proxy);
        value = proxy.Value;
    }
}