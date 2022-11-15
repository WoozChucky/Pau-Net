using Pau.Transport;

namespace Pau.App;

public sealed class PauService : IHostedService, IDisposable
{
    private readonly ITransport _transport;
    
    public PauService(ITransport transport)
    {
        _transport = transport ?? throw new ArgumentNullException(nameof(transport));
    }
    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Service Started...");
            
        _transport.Start();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Service Stopped...");

        _transport?.Stop();

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _transport.Dispose();
    }
}
