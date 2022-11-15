namespace Pau.Transport;

public abstract class BaseTransport : ITransport
{
    protected bool IsRunning;
    
    public virtual void Start()
    {
        IsRunning = true;
    }

    public virtual void Stop()
    {
        IsRunning = false;
    }
    
    public abstract void Dispose();
}
