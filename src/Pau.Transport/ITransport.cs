namespace Pau.Transport;

public interface ITransport : IDisposable
{
    void Start();
    void Stop();
}
