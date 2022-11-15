namespace Pau.App.Extensions;

public static class ServiceBaseLifetimeHostExtensions
{
    private static IHostBuilder UseServiceBaseLifetime(this IHostBuilder hostBuilder)
    {
        return hostBuilder.ConfigureServices((hostContext, services) => services.AddSingleton<IHostLifetime, ServiceBaseLifetime>());
    }

    private static Task RunAsServiceAsync(this IHostBuilder hostBuilder, CancellationToken cancellationToken = default)
    {
        return hostBuilder.UseServiceBaseLifetime().Build().RunAsync(cancellationToken);
    }

    /// <summary>
    /// Builds as service.
    /// </summary>
    /// <param name="hostBuilder">The host builder.</param>
    /// <returns></returns>
    public static IHost BuildAsService(this IHostBuilder hostBuilder)
    {
        return hostBuilder.UseServiceBaseLifetime().Build();
    }

    /// <summary>
    /// Builds as console.
    /// </summary>
    /// <param name="hostBuilder">The host builder.</param>
    /// <returns></returns>
    public static IHost BuildAsConsole(this IHostBuilder hostBuilder)
    {
        return hostBuilder.UseConsoleLifetime().Build();
    }
}
