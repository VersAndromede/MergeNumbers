using System;

namespace Ad
{
    public interface IAd
    {
        public event Action<bool> AdRunning;
    }
}