namespace PxtlCa.Collections.Proxies;

public static class IObjectProxyExtensions {
    public static void ThrowIfValNull<T>(this IObjectProxy<T>? proxy, string? parameterName = null, string? exceptionMessage = null) {
        Ensure.NotNull(proxy, parameterName ?? nameof(proxy));
        throw new NullProxyValueException(exceptionMessage ?? $"Cannot complete operation. Proxy member '{proxy!.Value}' is null.");
    }
}
