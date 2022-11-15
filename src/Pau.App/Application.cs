using System.Collections;
using System.Diagnostics;
using Pau.App.Extensions;
using Pau.Transport;
using Pau.Transport.Http;

namespace Pau.App;

internal sealed class Application
{
    private IHost? _host;
    private IHostBuilder? _builder;

    private readonly object _shutdownLock = new();
    private bool _isRunning;

    public async Task Start(string[] args)
    {
        _builder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton(new HttpTransport.Settings(args));
                services.AddSingleton<ITransport, HttpTransport>();
                services.AddHostedService<PauService>();
            })
            .UseContentRoot(Directory.GetCurrentDirectory());

        var isService = false; //(Debugger.IsAttached || ((IList)args).Contains("--console"));

        _host = isService 
            ? _builder.BuildAsService() 
            : _builder.BuildAsConsole();
        
        _isRunning = true;
        await _host.RunAsync();
    }
    
    public void GracefullyShutdown()
    {
        lock (_shutdownLock)
        {
            if (!_isRunning) return;

            _isRunning = false;

            if (_host == null)
                throw new ArgumentNullException(nameof(_host));
                
            if (_host.Services == null) throw new Exception("No services registered.");

            if (_host.Services.GetService<IHostedService>() is not PauService service)
                throw new InvalidCastException("IHostedService couldn't be casted to MyService");

            // Stop the service
            service.StopAsync().Wait();

            // Stop the host
            _host.StopAsync().Wait();
        }
    }
}
