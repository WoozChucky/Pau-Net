using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Pau.Transport.Http;

public class HttpTransport : BaseTransport
{

    public class Settings
    {
        public Settings(string[] args)
        {
            Arguments = args;
        }
        public string[] Arguments { get; }
    }
    
    private readonly IWebHost _httpServer;

    public HttpTransport(Settings settings)
    {
        _httpServer = WebHost.CreateDefaultBuilder(settings.Arguments)
            .ConfigureKestrel((_, options) =>
            {
                options.ListenAnyIP(5001, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
                    listenOptions.UseHttps();
                    listenOptions.UseConnectionLogging();
                });
            })
            // .UseKestrel()
            .CaptureStartupErrors(true)
            .UseStartup<HttpStartup>()
            .Build();
        
    }

    public override async void Start()
    {
        base.Start();
        await _httpServer.StartAsync();
    }

    public override async void Stop()
    {
        base.Stop();
        await _httpServer.StopAsync();
    }

    public override void Dispose()
    {
        _httpServer?.Dispose();
    }
}
