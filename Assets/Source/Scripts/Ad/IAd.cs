using System;

namespace Scripts.Ad
{
    public interface IAd
    {
        public event Action AdStarted;
        public event Action AdEnded;
    }
}