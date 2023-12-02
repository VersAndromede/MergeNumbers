using System;

public interface IAd
{
    public event Action<bool> AdRunning;
}
